using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class FeededAction : MonoBehaviour
{
    [SerializeField]
    private bool _startOnEnable = false;
    [SerializeField]
    private float _fullTime = 0.2f;
    [SerializeField]
    private bool _ignoreTimeScale = false;

    [SerializeField]
    private UnityEvent _firstTimeFeededAction;
    [SerializeField]
    private UnityEvent _onFeededAction;
    [SerializeField]
    private UnityEvent _onHungryAction;

    [Header("Inspec")]
    [SerializeField]
    private float _hungryAfter;

    private bool _tempDisable;
    private bool _initFeed = true;

    private CancellationTokenSource updateToken;

    public void Feed()
    {
        if (_initFeed)
        {
            InitFeed();
            _initFeed = false;
        }

        _hungryAfter = _fullTime;
        _tempDisable = false;

        _onFeededAction.Invoke();
    }

    public void PermanentDisableAction()
    {
        updateToken?.Cancel();
        _initFeed = true;
    }

    private void InitFeed()
    {
        TrackLifeTime().Forget();
        _firstTimeFeededAction.Invoke();
    }

    private async UniTaskVoid TrackLifeTime()
    {
        updateToken?.Cancel();
        updateToken = new();

        await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(updateToken.Token))
        {
            if (_hungryAfter <= 0f && !_tempDisable)
            {
                _tempDisable = true;
                _onHungryAction.Invoke();
            }

            _hungryAfter -= _ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        _initFeed = true;
        if (_startOnEnable)
        {
            Feed();
        }
    }

    private void OnDisable()
    {
        PermanentDisableAction();
    }
}
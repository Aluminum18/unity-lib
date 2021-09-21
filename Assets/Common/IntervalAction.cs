using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class IntervalAction : MonoBehaviour
{
    [SerializeField]
    private bool _startOnEnable = false;
    [SerializeField]
    private float _interval;
    public float Interval
    {
        set
        {
            _interval = value;
        }
    }
    [SerializeField]
    private UnityEvent _action;

    private CompositeDisposable _cd = new CompositeDisposable();

    public void StartIntervalAction()
    {
        if (_interval <= 0f)
        {
            return;
        }

        _cd.Clear();
        Observable.Interval(System.TimeSpan.FromSeconds(_interval)).Subscribe(_ =>
        {
            _action.Invoke();
        }).AddTo(_cd);
    }

    public void StopIntervalAction()
    {
        _cd.Clear();
    }

    private void OnEnable()
    {
        if (!_startOnEnable)
        {
            return;
        }
        StartIntervalAction();
    }

    private void OnDisable()
    {
        _cd.Clear();
    }
}

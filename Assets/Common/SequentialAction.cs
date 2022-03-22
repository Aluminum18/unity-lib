using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class SequentialAction : MonoBehaviour
{
    [SerializeField]
    private bool _startOnEnable = false;

    [SerializeField]
    private List<ActionInSequence> _actions;


    public async UniTask StartActions()
    {
        for (int i = 0; i < _actions.Count; i++)
        {
            var action = _actions[i];
            await UniTask.Delay(System.TimeSpan.FromSeconds(action.startAfter));
            action.action.Invoke();
        }
    }

    private async void OnEnable()
    {
        if (_startOnEnable)
        {
            await StartActions();
        }
    }
}

[System.Serializable]
public class ActionInSequence
{
    public UnityEvent action;
    public float startAfter;
}

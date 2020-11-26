using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent _gameEvent;
    [SerializeField]
    private UnityEvent _handler;

    private void OnEnable()
    {

        _gameEvent.Subcribe(HandleEvent);
    }

    private void OnDisable()
    {
        _gameEvent.Unsubcribe(HandleEvent);
    }

    private void HandleEvent(params object[] args)
    {
        _handler.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjectSystem/GameEvent")]
public class GameEvent : ScriptableObject
{
    public delegate void EventActionDel(params object[] eventParam);
    private event EventActionDel _eventAction;

    public void Subcribe(EventActionDel action)
    {
        _eventAction += action;
    }

    public void Unsubcribe(EventActionDel action)
    {
        _eventAction -= action;
    }

    public void Raise(params object[] eventParam)
    {
        _eventAction?.Invoke(eventParam);
    }
}

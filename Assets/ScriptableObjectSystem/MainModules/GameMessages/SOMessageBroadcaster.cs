using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOMessageBroadcaster : MonoBehaviour
{
    [SerializeField]
    private bool _logWhenBroadcastMessage = false;
    [SerializeField]
    private SOMessage[] _managedMessages;
    private Dictionary<SOMessage, List<(MonoBehaviour, Action)>> _messageAndActionDict = new();

    public void BroadcastMessage(SOMessage message)
    {
        _messageAndActionDict.TryGetValue(message, out var actions);
        if (actions == null)
        {
            Debug.LogWarning($"message {message.name} does not register any action on this broadcaster", this);
            return;
        }
        
        if (actions == null)
        {
            actions = new();
        }
        for (int i = 0; i < actions.Count; i++)
        {
            MonoBehaviour associatedObj = actions[i].Item1;
            if (associatedObj == null || !associatedObj.enabled)
            {
                continue;
            }
            if (_logWhenBroadcastMessage)
            {
                Debug.Log($"Message [{message.name}] has been sent to [{associatedObj.gameObject.name}]");
            }

            actions[i].Item2.Invoke();
        }
    }

    public void BroadcastAllMessage()
    {
        for (int i = 0; i < _managedMessages.Length; i++)
        {
            BroadcastMessage(_managedMessages[i]);
        }
    }

    public void SetUpMessageAction(SOMessage message, MonoBehaviour associatedObject, Action action)
    {
        _messageAndActionDict.TryGetValue(message, out var actions);
        if (actions == null)
        {
            actions = new();
            _messageAndActionDict.Add(message, actions);
        }

        var messageAndAction = (associatedObject, action);
        if (actions.Contains(messageAndAction))
        {
            return;
        }

        actions.Add(messageAndAction);
    }
}

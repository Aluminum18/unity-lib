using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOMessageBroadcaster : MonoBehaviour
{
    [SerializeField]
    private bool _logWhenBroadcastMessage = false;
    [SerializeField]
    private List<SOMessage> _managedMessages;
    private Dictionary<SOMessage, List<(MonoBehaviour, SOMessage.SOMessageAction)>> _messageAndActionDict = new();

    public void BroadcastMessage(SOMessage message, params object[] args)
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

            if (args == null || args.Length == 0)
            {
                actions[i].Item2.Invoke();
                return;
            }
            actions[i].Item2.Invoke(args);
        }
    }

    public void BroadcastMessage(SOMessage message)
    {
        BroadcastMessage(message, null);
    }

    public void SetUpMessageAction(SOMessage message, MonoBehaviour associatedObject, SOMessage.SOMessageAction action)
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

#if UNITY_EDITOR
    public void EditorOnly_AddMessageFromListener(SOMessage message)
    {
        if (_managedMessages.Contains(message))
        {
            return;
        }

        _managedMessages.Add(message);
    }

    public void EditorOnly_BroadcastAllMessage()
    {
        for (int i = 0; i < _managedMessages.Count; i++)
        {
            BroadcastMessage(_managedMessages[i]);
        }
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SOMessageListener : MonoBehaviour
{
    [SerializeField]
    private SOMessage[] _listenedMessages;
    public UnityEvent _onReceivedMessage;

    private void Start()
    {
        for (int i = 0; i < _listenedMessages.Length; i++)
        {
            _listenedMessages[i].Listen(MessageHandler, this);
        }
    }

    private void MessageHandler()
    {
        _onReceivedMessage.Invoke();
    }
}

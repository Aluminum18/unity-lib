using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRaiser : MonoBehaviour
{
    [SerializeField]
    private GameEvent _testEvent;

    public void RaiseEventNoParam()
    {
        _testEvent.Raise();
    }
}

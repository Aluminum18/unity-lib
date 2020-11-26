using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScriptableObjectVariable <T> : ScriptableObject
{
    [SerializeField]
    protected T _value;

    public delegate void OnValueChangedDel(T newValue);
    public event OnValueChangedDel OnValueChange;

    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (!IsSetNewValue(value))
            {
                return;
            }
            _value = value;

            OnValueChange?.Invoke(value);
        }
    }

    protected virtual bool IsSetNewValue(T value)
    {
        return true;
    }
}

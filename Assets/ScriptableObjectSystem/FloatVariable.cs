using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVar", menuName = "ScriptableObjectSystem/FloatVariable")]
public class FloatVariable : BaseScriptableObjectVariable<float>
{
    protected override bool IsSetNewValue(float value)
    {
        return value != _value;
    }
}

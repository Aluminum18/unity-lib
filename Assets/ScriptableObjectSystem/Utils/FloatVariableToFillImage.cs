using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatVariableToFillImage : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _refFloat;
    [SerializeField]
    private Image _targetImage;

    private void OnEnable()
    {
        _refFloat.OnValueChange += UpdateImageFill;

    }

    private void OnDisable()
    {
        _refFloat.OnValueChange -= UpdateImageFill;
    }

    private void UpdateImageFill(float value)
    {
        _targetImage.fillAmount = value;
    }
}

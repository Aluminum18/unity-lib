using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMaterialProperty : MonoBehaviour
{
    [SerializeField]
    private bool _getRenderersFromRootObject = false;
    [SerializeField]
    private GameObject _rootObject;
    [SerializeField]
    private Renderer[] _renderers;
    [SerializeField]
    private Ease _tweenType;
    [SerializeField]
    private float _duration = 0.5f;

    [Header("Inspec")]
    [SerializeField]
    private string _targetProps;

    public void SetTarget(string propName)
    {
        _targetProps = propName;
    }

    public void SetInitPropValue(float init)
    {
        SetFloat(init);
    }

    public void SetFloat(float value)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            var renderer = _renderers[i];
            if (!renderer.material.HasFloat(_targetProps))
            {
                continue;
            }
            renderer.material.SetFloat(_targetProps, value);
        }
    }

    public void TweenFloat(float to)
    {
        TweenFloat(float.NaN, to, _duration, _targetProps);
    }

    public void TweenFloat(float from, float to, float duration, string propsName)
    {
        if (string.IsNullOrEmpty(propsName))
        {
            propsName = _targetProps;
        }

        if (duration == 0f)
        {
            SetFloat(to);
            return;
        }

        if (_tweenType == Ease.Unset)
        {
            SetFloat(to);
            return;
        }

        for (int i = 0; i < _renderers.Length; i++)
        {
            var material = _renderers[i].material;
            if (!material.HasFloat(propsName))
            {
                continue;
            }

            float current = float.IsNaN(from) ? material.GetFloat(propsName) : from;
            DOTween.To(() => current, value => current = value, to, duration).SetEase(_tweenType).onUpdate = () =>
            {
                material.SetFloat(propsName, current);
            };
        }
    }

    private void OnEnable()
    {
        if (_getRenderersFromRootObject)
        {
            _renderers = _rootObject.GetComponentsInChildren<Renderer>();
        }
    }
}

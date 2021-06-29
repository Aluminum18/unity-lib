using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class LeanTweenAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _doTweenOnEnable = false;
    [SerializeField]
    private GameObject _go;
    [SerializeField]
    private AnimType _animType;
    [SerializeField]
    private LeanTweenType _tweenType;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private bool _useCurrentAsFrom = false;
    [SerializeField]
    private Vector3 _from;
    [SerializeField]
    private Vector3 _to;

    public enum AnimType
    {
        None = 0,
        Move,
        Zoom,
        FadeSprite,
        FadeUI,
        MoveLocal
    }
    public void DoTweenWithDelay(float delay)
    {
        Observable.Timer(System.TimeSpan.FromSeconds(delay)).Subscribe(_ =>
        {
            DoTween();
        });
    }

    public void DoTween()
    {
        if (_go == null)
        {
            _go = gameObject;
        }

        switch (_animType)
        {
            case AnimType.Move:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.position = _from;
                    }

                    LeanTween.move(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.MoveLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localPosition = _from;
                    }
                    LeanTween.moveLocal(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.Zoom:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localScale = _from;
                    }
                    LeanTween.scale(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.FadeSprite:
                {
                    var spriteRenderer = _go.GetComponent<SpriteRenderer>();

                    if (spriteRenderer == null)
                    {
                        return;
                    }
                    if (!_useCurrentAsFrom)
                    {
                        Color bufferColor = spriteRenderer.color;
                        bufferColor.a = _from.x;
                        spriteRenderer.color = bufferColor;
                    }

                    LeanTween.alpha(_go, _to.x, _duration);
                    break;
                }
            case AnimType.FadeUI:
                {
                    //later
                    break;
                }
            default:
                break;
        }
    }

    private void OnEnable()
    {
        if (_doTweenOnEnable)
        {
            DoTween();
        }
    }
}

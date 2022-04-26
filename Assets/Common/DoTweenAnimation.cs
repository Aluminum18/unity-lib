using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DoTweenAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _doTweenOnEnable = false;
    [SerializeField]
    private GameObject _go;
    [SerializeField]
    private AnimType _animType;
    [SerializeField]
    private Ease _easeType;
    [SerializeField]
    private float _duration;
    public float Duration
    {
        set
        {
            _duration = value;
        }
    }
    [SerializeField]
    private bool _useCurrentAsFrom = false;
    [SerializeField]
    private Vector3 _from;
    [SerializeField]
    private Vector3 _to;

    [SerializeField]
    private UnityEvent _onStartTween;
    [SerializeField]
    private UnityEvent _onFinishTween;

    public enum AnimType
    {
        None = 0,
        Move,
        Zoom,
        FadeSprite,
        FadeUI,
        MoveLocal,
        RotateLocal
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

        _onStartTween.Invoke();

        switch (_animType)
        {
            case AnimType.Move:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.position = _from;
                    }

                    _go.transform.DOMove(_to, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
                    break;
                }
            case AnimType.MoveLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localPosition = _from;
                    }
                    _go.transform.DOLocalMove(_to, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
                    break;
                }
            case AnimType.Zoom:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localScale = _from;
                    }
                    _go.transform.DOScale(_to, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
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

                    spriteRenderer.DOFade(_to.x, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
                    break;
                }
            case AnimType.FadeUI:
                {
                    var canvasGroup = _go.GetComponent<CanvasGroup>();

                    if (canvasGroup == null)
                    {
                        Debug.LogWarning("To use FadeUI, add CanvasGroup component to the target");
                        return;
                    }

                    if (!_useCurrentAsFrom)
                    {
                        canvasGroup.alpha = _from.x;
                    }

                    canvasGroup.DOFade(_to.x, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
                    break;
                }
            case AnimType.RotateLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localRotation = Quaternion.Euler(_from);
                    }

                    _go.transform.DOLocalRotate(_to, _duration).SetEase(_easeType).onComplete = () => _onFinishTween.Invoke();
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

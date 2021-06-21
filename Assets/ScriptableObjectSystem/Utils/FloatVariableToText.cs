using UnityEngine;
using TMPro;
using UniRx;

public class FloatVariableToText : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _floatVariable;
    [SerializeField]
    private TMP_Text _textMesh;
    [SerializeField]
    private bool _continuousChange = false;
    [SerializeField]
    private float _unitChangePerSec;
    [SerializeField]
    private float _continuousChangeCapTime;
    [SerializeField]
    private string _format = "0.0";
    [SerializeField]
    private string _additionFormat = "{0}";

    private CompositeDisposable _cd = new CompositeDisposable();
    private float _lastValue;

    private void Start()
    {
        _textMesh.text = string.Format(_additionFormat, _floatVariable.Value.ToString(_format));
        _floatVariable.OnValueChange += UpdateValue;
    }

    private void OnDestroy()
    {
        _floatVariable.OnValueChange -= UpdateValue;
    }

    private void UpdateValue(float newValue)
    {
        if (_continuousChange)
        {
            ContinuousUpdateValue(newValue);
            return;
        }

        _textMesh.text = string.Format(_additionFormat, _floatVariable.Value.ToString(_format));
    }

    private void ContinuousUpdateValue(float newValue)
    {
        _cd.Clear();

        float diff = newValue - _lastValue;

        float expectedTime = Mathf.Abs(diff) / _unitChangePerSec;

        float changePerSec = _unitChangePerSec * Mathf.Sign(diff);
        if (expectedTime > _continuousChangeCapTime)
        {
            changePerSec = diff / _continuousChangeCapTime;
            expectedTime = _continuousChangeCapTime;
        }

        float bufferValue = _lastValue;
        _lastValue = newValue;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            bufferValue += changePerSec * Time.deltaTime;
            if (expectedTime < 0f)
            {
                _textMesh.text = string.Format(_additionFormat, _floatVariable.Value.ToString(_format));
                _cd.Clear();
                return;
            }
            _textMesh.text = string.Format(_additionFormat, bufferValue.ToString(_format));
            expectedTime -= Time.deltaTime;
        }).AddTo(_cd);
    }
}

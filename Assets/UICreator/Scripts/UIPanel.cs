using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private List<UIElement> _elements;
    [SerializeField]
    private bool _showFromStart;

    [Header("UnityEvents")]
    [SerializeField]
    private UnityEvent _onAllElementsShown;
    [SerializeField]
    private UnityEvent _onAllElementsHided;

    private Canvas _canvas;
    private GraphicRaycaster _rayCaster;

    [SerializeField]
    private int _showedElements = 0;

    public bool ShowFromStart
    {
        get
        {
            return _showFromStart;
        }
    }

    public void Init()
    {
        _canvas = GetComponent<Canvas>();
        if (_canvas == null)
        {
            Debug.LogError($"Missing canvas of [{gameObject.name}]", this);
            return;
        }

        _rayCaster = GetComponent<GraphicRaycaster>();
        if (_rayCaster == null)
        {
            Debug.LogWarning($"Missing graphic raycaster of [{gameObject.name}]", this);
        }
        else
        {
            _rayCaster.enabled = false;
        }

        if (!ValidateElements())
        {
            return;
        }

        _canvas.enabled = false;

        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].Init(this);
        }
    }

    public void Open()
    {
        if (!ValidateElements())
        {
            return;
        }

        _canvas.enabled = true;
        transform.SetAsLastSibling();
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].Show();
        }
    }

    public void Close()
    {
        if (!ValidateElements())
        {
            return;
        }
        transform.SetAsFirstSibling();

        _rayCaster.enabled = false;

        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].Hide();
        }
    }

    public void OpenOther(UIElement other)
    {
        other.Show();
    }

    public void CloseAndOpenOther(UIElement other)
    {
        Close();
        OpenOther(other);
    }

    public void NotifyElementShowed()
    {
        _showedElements++;
        if (_showedElements == _elements.Count)
        {
            _rayCaster.enabled = true;
            _onAllElementsShown?.Invoke();
        }
    }

    public void NotifyElementHided()
    {
        _showedElements--;
        if (_showedElements == 0)
        {
            _canvas.enabled = false;
            _onAllElementsHided?.Invoke();
        }
    }

    private bool ValidateElements()
    {
        if (_elements.Count == 0)
        {
            Debug.LogWarning($"No element found in {gameObject.name}", this);
            return false;
        }

        return true;
    }
}

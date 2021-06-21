using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private List<UIPanel> _UIPanels;

    // Store UI panel following opening order
    private Stack<UIPanel> _panelStack = new Stack<UIPanel>();

    public void PushToStack(UIPanel panel)
    {
        if (_panelStack.Count > 0)
        {
            var recentPanel = _panelStack.Peek();
            recentPanel.SetInteractable(!recentPanel.IsOpening);
        }

        _panelStack.Push(panel);
    }

    public void PopFromStack()
    {
        if (_panelStack.Count == 0)
        {
            return;
        }

        _panelStack.Pop();

        if (_panelStack.Count == 0)
        {
            return;
        }

        var recentPanel = _panelStack.Peek();
        if (recentPanel.IsOpening)
        {
            recentPanel.SetInteractable(true);
        }
        else
        {
            PopFromStack();
        }
    }

    private void Start()
    {
        InitAllUIPanels();
    }

    private void InitAllUIPanels()
    {
        for (int i = 0; i < _UIPanels.Count; i++)
        {
            var panel = _UIPanels[i];
            if (panel == null)
            {
                // Log
                continue;
            }

            panel.Init(this);

            if (panel.ShowFromStart)
            {
                panel.Open();
            }
        }
    }

    public void InitUIPanels(List<UIPanel> panels)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            var panel = panels[i];
            if (panel == null)
            {
                // Log
                continue;
            }

            panel.Init(this);

            if (panel.ShowFromStart)
            {
                panel.Open();
            }
        }
    }

    public void InitUIPanel(UIPanel panel)
    {
        if (panel == null)
        {
            // Log
            return;
        }

        panel.Init(this);

        if (panel.ShowFromStart)
        {
            panel.Open();
        }
    }

    public void CloseLastPanel()
    {
        if (_panelStack.Count == 0)
        {
            return;
        }

        var recentPanel = _panelStack.Peek();
        if (recentPanel.IsOpening)
        {
            recentPanel.Close();
        }
    }

    public void CloseAllPanel()
    {
        foreach (var item in _panelStack)
        {
            item.Close();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private List<UIPanel> _UIPanels;

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

            panel.Init();

            if (panel.ShowFromStart)
            {
                panel.Open();
            }
        }
    }
}

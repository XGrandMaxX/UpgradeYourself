using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PanelNavigator
{
    private readonly Dictionary<Button, GameObject> _buttonPanelMap = new();

    public PanelNavigator(Dictionary<Button, GameObject> buttonPanelMap)
    {
        _buttonPanelMap = buttonPanelMap;

        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        Button button;
        GameObject panel;

        foreach (var entry in _buttonPanelMap)
        {
            button = entry.Key;
            panel = entry.Value;

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() =>
            {
                ShowPanel(panel);
            });
        }
    }

    private void ShowPanel(GameObject panel)
    {
        foreach (var p in _buttonPanelMap.Values)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);
    }
}

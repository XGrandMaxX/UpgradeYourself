using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class UIPanelConfig
{
    [field: SerializeField] private string configName { get; set; }

    public Button Button;
    public GameObject Panel;

    public UIPanelConfig(Button button, GameObject panel)
    {
        Button = button;
        Panel = panel;
    }
}

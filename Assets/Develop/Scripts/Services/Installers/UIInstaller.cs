using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private List<UIPanelConfig> panelConfigs;

    public override void InstallBindings() => Bind();

    private void Bind()
    {
        var buttonPanelMap = new Dictionary<Button, GameObject>();

        foreach (var config in panelConfigs)
        {
            buttonPanelMap[config.Button] = config.Panel;
        }

        Container.Bind<PanelNavigator>()
            .AsSingle()
            .WithArguments(buttonPanelMap)
            .NonLazy();
    }
}

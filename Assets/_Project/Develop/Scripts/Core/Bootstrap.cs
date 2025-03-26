using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

[DefaultExecutionOrder(-200)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField, Scene] private string _loadingScene;

    private async void Awake()
    {
        InitializeServices();

        Debug.Log("<color=yellow>[Bootstrap]</color>: Игра инициализирована!");
        Debug.Log("<color=yellow>[Bootstrap]</color>: Запуск!");

        await StartGame();
    }

    private void InitializeServices()
    {
        G.SceneLoader = SceneLoader.Instance;
        G.ScreenFader = UIFader.Instance;
    }

    private async UniTask StartGame() => await G.SceneLoader.Load(_loadingScene);
}

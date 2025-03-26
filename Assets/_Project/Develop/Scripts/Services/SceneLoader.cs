using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader : ILoader
{
    private static SceneLoader _instance;
    private SceneLoader() { }

    public static SceneLoader Instance => _instance ??= new SceneLoader();
    public async UniTask Load(string path) => await SceneManager.LoadSceneAsync(path);
}

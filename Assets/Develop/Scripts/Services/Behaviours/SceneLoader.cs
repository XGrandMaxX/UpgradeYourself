using Cysharp.Threading.Tasks;
using Develop.Scripts.Services.Abstractions;
using UnityEngine.SceneManagement;

namespace Develop.Scripts.Services.Behaviours
{
    public sealed class SceneLoader : ISceneLoader
    {
        public async UniTask LoadScene(string path)
        {
            await SceneManager.LoadSceneAsync(path);
        }
    }
}

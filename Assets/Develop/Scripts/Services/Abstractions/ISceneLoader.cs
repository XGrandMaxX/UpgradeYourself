using Cysharp.Threading.Tasks;

namespace Develop.Scripts.Services.Abstractions
{
    public interface ISceneLoader
    {
        public UniTask LoadScene(string path);
    }
}

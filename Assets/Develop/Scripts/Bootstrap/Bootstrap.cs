using Develop.Scripts.Services.Abstractions;
using Zenject;

namespace Develop.Scripts.Bootstrap
{
    public sealed class Bootstrap
    {
        private readonly ISceneLoader _sceneLoader;
        
        public Bootstrap(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        [Inject]
        public async void Construct()
        {
            await _sceneLoader.LoadScene("MainMenu");
        }
    }
}

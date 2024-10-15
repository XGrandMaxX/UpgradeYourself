using Develop.Scripts.Services.Abstractions;
using UnityEngine;
using Zenject;

namespace Develop.Scripts.Bootstrap
{
    public sealed class Bootstrap
    {
        private readonly ISceneLoader _sceneLoader;
        
        public Bootstrap(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            Debug.Log(1);
        }

        [Inject]
        public async void Construct()
        {
            await _sceneLoader.LoadScene("MainMenu");
        }
    }
}

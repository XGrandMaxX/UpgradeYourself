using Develop.Scripts.Services.Abstractions;
using Develop.Scripts.Services.Behaviours;
using Zenject;

namespace Develop.Scripts.Services.Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind();
        }

        private void Bind()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
            
            Container.Bind<Bootstrap.Bootstrap>()
                .AsSingle()
                .NonLazy();
        }
    }
}

using DB;
using Zenject;

namespace Installers
{
    public sealed class DBConnectorInstaller : MonoInstaller
    {
        public override void InstallBindings() 
            => Container.Bind<DBConnector>().AsSingle().NonLazy();
    }
}
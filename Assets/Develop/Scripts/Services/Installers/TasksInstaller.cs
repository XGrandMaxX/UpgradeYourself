using Develop.Scripts.Data.Repositories;
using Develop.Scripts.Managers;
using Develop.Scripts.Services.Systems;
using Zenject;

namespace Develop.Scripts.Services.Installers
{
    public sealed class TasksInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindDependencies();

        private void Bind<T>() where T : class => Container.Bind<T>().AsSingle().NonLazy();

        private void BindDependencies()
        {
            Bind<TaskFactory>();
            Bind<TaskLoader>();
            Bind<RewardManager>();
            Bind<TaskCreationManager>();
        }
    }
}

using Zenject;

public class UserInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container.Bind<MyUserProfile>()
            .AsSingle()
            .NonLazy();
    }
}
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlanetPopupView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<PlanetView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
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
                .Bind<CoinDepositView>()
                .FromComponentInHierarchy()
                .AsCached();
            Container
                .Bind<ParticlesView>()
                .FromComponentInHierarchy()
                .AsCached();
            Container
                .Bind<GameView>()
                .FromComponentInHierarchy()
                .AsCached();
        }
    }
}
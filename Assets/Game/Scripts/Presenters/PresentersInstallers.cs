using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlanetPopupPresenter>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<GamePresenter>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ParticlesPresenter>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<CoinDepositPresenter>()
                .AsSingle();
            
            BindPlanetPresenterFactory<PlanetPresenter>();
            BindPlanetPresenterFactory<TimeProgressPresenter>();
            BindPlanetPresenterFactory<CoinPresenter>();
        }

        private void BindPlanetPresenterFactory<TPresenter>() where TPresenter : IPresenter
        {
            Container
                .BindFactory<IPlanet, TPresenter, PlanetPresenterFactory<TPresenter>>()
                .AsSingle();
        }
    }
}
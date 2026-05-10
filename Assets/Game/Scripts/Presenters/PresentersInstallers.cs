using System.Collections.Generic;
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
                .BindInterfacesAndSelfTo<CoinDepositPresenter>()
                .AsSingle();
            Container
                .BindFactory<IPlanet, PlanetPresenter, PlanetPresenter.Factory>()
                .AsSingle();
        }
        
        
    }
}
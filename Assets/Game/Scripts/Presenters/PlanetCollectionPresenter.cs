using System.Collections.Generic;
using Modules.Planets;
using ObservableCollections;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetCollectionPresenter : IPresenter
    {
        public IObservableCollection<PlanetPresenter> PlanetPresenters => _planetPresenters;
        private readonly ObservableList<PlanetPresenter> _planetPresenters = new();

        private readonly PlanetPresenterFactory<PlanetPresenter> _planetPresenterFactory;
        private readonly List<IPlanet> _planets;

        [Inject]
        public PlanetCollectionPresenter(
            List<IPlanet> planets,
            PlanetPresenterFactory<PlanetPresenter> planetPresenterFactory
        )
        {
            _planets = planets;
            _planetPresenterFactory = planetPresenterFactory;
        }

        void IInitializable.Initialize()
        {
            foreach (var planet in _planets)
            {
                var planetPresenter = _planetPresenterFactory.Create(planet);
                _planetPresenters.Add(planetPresenter);
            }
        }
    }
}
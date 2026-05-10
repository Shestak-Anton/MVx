using System.Collections.Generic;
using Modules.Planets;
using ObservableCollections;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class GamePresenter : IInitializable
    {
        private readonly PlanetPresenter.Factory _planetPresenterFactory;
        private readonly List<IPlanet> _planets;

        public IObservableCollection<PlanetPresenter> Presenters => _presenters;
        private readonly ObservableList<PlanetPresenter> _presenters = new();

        [Inject]
        public GamePresenter(List<IPlanet> planets, PlanetPresenter.Factory planetPresenterFactory)
        {
            _planets = planets;
            _planetPresenterFactory = planetPresenterFactory;
        }

        void IInitializable.Initialize()
        {
            foreach (var planet in _planets)
            {
                var planetPresenter = _planetPresenterFactory.Create(planet);
                _presenters.Add(planetPresenter);
            }
        }
    }
}
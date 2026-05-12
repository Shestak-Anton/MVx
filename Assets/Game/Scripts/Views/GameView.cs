using System;
using Game.Presenters;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class GameView : MonoBehaviour
    {
        [SerializeField] private PlanetsContainer _planetsContainer;
        [SerializeField] private ParticlesView _particlesView;

        private GamePresenter _gamePresenter;
        private ParticlesPresenter _particlesPresenter;

        [Inject]
        public void Build(GamePresenter gamePresenter, ParticlesPresenter particlesPresenter)
        {
            _gamePresenter = gamePresenter;
            _particlesPresenter = particlesPresenter;
        }

        private void Awake()
        {
            _gamePresenter.PlanetPresenters
                .ObserveAdd()
                .Subscribe(it => InitPlanets(it.Value, it.Index))
                .AddTo(this);
        }

        private void InitPlanets(PlanetPresenter presenter, int index)
        {
            var views = _planetsContainer.Planets;
            if (index >= views.Count)
                throw new Exception("Views Count is not equal to planetPresenters.Count");
            var planetView = views[index];
            planetView.Init(presenter);
            _particlesPresenter.AddPoint((presenter.Planet, planetView.CoinPosition));
        }
    }
}
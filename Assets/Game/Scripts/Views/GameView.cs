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

        private GamePresenter _gamePresenter;

        [Inject]
        public void Build(GamePresenter gamePresenter)
        {
            _gamePresenter = gamePresenter;
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
            views[index].Init(presenter);
        }
    }
}
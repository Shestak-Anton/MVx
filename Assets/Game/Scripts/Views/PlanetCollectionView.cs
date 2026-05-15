using System;
using System.Collections.Generic;
using Game.Presenters;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class PlanetCollectionView : MonoBehaviour
    {
        [SerializeField] private List<PlanetView> _planets;
        [SerializeField] private ParticlesView _particlesView;

        private PlanetCollectionPresenter _planetCollectionPresenter;

        [Inject]
        public void Construct(PlanetCollectionPresenter planetCollectionPresenter) =>
            _planetCollectionPresenter = planetCollectionPresenter;

        private void Awake()
        {
            _planetCollectionPresenter.PlanetPresenters
                .ObserveAdd()
                .Subscribe(it => InitPlanets(it.Value, it.Index))
                .AddTo(this);
        }

        private void InitPlanets(PlanetPresenter presenter, int index)
        {
            if (index >= _planets.Count)
                throw new Exception("Views Count is not equal to planetPresenters.Count");
            var planetView = _planets[index];
            planetView.Init(presenter);
        }
    }
}
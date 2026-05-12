using System.Collections.Generic;
using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class ParticlesPresenter : IInitializable
    {
        private readonly ReactiveCommand<Vector3> _triggerParticleFrom = new();
        public Observable<Vector3> TriggerParticleFrom => _triggerParticleFrom;

        private readonly Dictionary<IPlanet, Vector3> _planets = new();

        public void AddPoint((IPlanet planet, Vector3 position) planet)
        {
            _planets.Add(planet.planet, planet.position);
        }

        void IInitializable.Initialize()
        {
            foreach (var planet in _planets.Keys)
            {
                planet.OnGathered += _ => OnGathered(planet);
            }
        }

        private void OnGathered(IPlanet planet)
        {
            if (_planets.TryGetValue(planet, out var position))
                _triggerParticleFrom.Execute(position);
        }
    }
}
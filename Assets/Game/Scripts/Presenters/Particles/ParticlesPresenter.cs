using System.Collections.Generic;
using DG.Tweening;
using Modules.Planets;
using R3;
using UnityEngine;

namespace Game.Presenters
{
    public sealed class ParticlesPresenter
    {
        private readonly ReactiveCommand<(Vector3 position, string particleId)> _particlePosition = new();
        public Observable<(Vector3 position, string particleId)> ParticlePosition => _particlePosition;

        private readonly ReactiveCommand<string> _onAnimationCompleted = new();
        public Observable<string> OnAnimationCompleted => _onAnimationCompleted;

        private readonly HashSet<IPlanet> _planets = new();

        private Vector3? _target;

        public void OnCoinGathered(Vector3 position, IPlanet planet)
        {
            _planets.Add(planet);
            if (_target.HasValue)
            {
                Move(position, _target.Value, planet.Name);
            }
        }

        public void OnTargetPositionReady(Vector3 position) => _target = position;

        private void Move(Vector3 from, Vector3 to, string animationId)
        {
            var progress = from;
            DOTween.To(
                    getter: () => progress,
                    setter: value =>
                    {
                        _particlePosition.Execute((progress, animationId));
                        progress = value;
                    },
                    duration: 0.5f,
                    endValue: to
                )
                .SetEase(Ease.OutExpo)
                .OnComplete(() => Gather(animationId))
                .SetId(animationId);
        }

        private void Gather(string animationId)
        {
            _onAnimationCompleted.Execute(animationId);
            foreach (var planet in _planets)
            {
                if (planet.Name == animationId)
                {
                    planet.GatherIncome();
                    break;
                }
            }
        }
    }
}
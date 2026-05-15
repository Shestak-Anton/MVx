using System.Collections.Generic;
using Game.Presenters;
using Modules.UI;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ParticlesView : MonoBehaviour
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private ParticleAnimator _particleAnimator;

        private readonly Dictionary<string, RectTransform> _particlesLookup = new();

        [Inject]
        public void Construct(ParticlesPresenter presenter)
        {
            this.LateUpdateAsObservable()
                .Take(1)
                .Subscribe(_ => presenter.OnTargetPositionReady(_target.position))
                .AddTo(this);
            presenter.ParticlePosition
                .Subscribe(OnParticlePositionChanged)
                .AddTo(this);
            presenter.OnAnimationCompleted
                .Subscribe(OnParticleAnimationCompleted)
                .AddTo(this);
        }

        private void OnParticlePositionChanged((Vector3 position, string id) pair)
        {
            var id = pair.id;
            var position = pair.position;
            if (_particlesLookup.TryGetValue(id, out var particle))
            {
                particle.position = position;
            }
            else
            {
                var rectTransform = _particleAnimator.GetParticle();
                rectTransform.position = position;
                _particlesLookup.Add(id, rectTransform);
            }
        }

        private void OnParticleAnimationCompleted(string id)
        {
            if (_particlesLookup.TryGetValue(id, out var particle))
            {
                _particleAnimator.Disable(particle);
                _particlesLookup.Remove(id);
            }
        }
    }
}
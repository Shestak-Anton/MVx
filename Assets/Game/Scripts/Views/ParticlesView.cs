using Game.Presenters;
using Modules.UI;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ParticlesView : MonoBehaviour
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private ParticleAnimator _particleAnimator;

        private ParticlesPresenter _presenter;

        [Inject]
        public void Construct(ParticlesPresenter presenter) => _presenter = presenter;

        private void Awake()
        {
            _presenter.TriggerParticleFrom
                .Subscribe(MoveParticle)
                .AddTo(this);
        }

        private void MoveParticle(Vector3 from)
        {
            _particleAnimator.Emit(from, _target.position);
        }
    }
}
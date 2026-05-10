using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPresenter : IPresenter
    {
        // locked
        private readonly ReactiveProperty<bool> _isUnlocked = new();

        public Observable<bool> IsUnlocked => _isUnlocked;

        // icon
        private readonly ReactiveProperty<Sprite> _sprite = new();

        public Observable<Sprite> Sprite => _sprite;

        // price
        private readonly ReactiveProperty<int> _price = new();
        public Observable<string> Price => _price.Select(it => $"{it}");
        
        private readonly ReactiveProperty<TimeProgressPresenter> _timeProgressPresenter = new();
        public Observable<TimeProgressPresenter> TimeProgressPresenter => _timeProgressPresenter;

        private readonly IPlanet _planet;
        private readonly PlanetPresenterFactory<TimeProgressPresenter> _timeProgressPresenterFactory;

        [Inject]
        public PlanetPresenter(
            IPlanet planet,
            PlanetPresenterFactory<TimeProgressPresenter> timeProgressPresenterFactory
        )
        {
            _planet = planet;
            _timeProgressPresenterFactory = timeProgressPresenterFactory;
        }

        public void OnLongPress()
        {
        }

        public void OnShortPress()
        {
            if (!_planet.CanUnlock) return;
            _planet.Unlock();
        }

        private void OnUnlocked()
        {
            _sprite.Value = _planet.GetIcon(true);
            _isUnlocked.Value = true;
        }

        public void Initialize()
        {
            _timeProgressPresenter.Value = _timeProgressPresenterFactory.Create(_planet);

            _sprite.Value = _planet.GetIcon(_planet.IsUnlocked);
            _isUnlocked.Value = _planet.IsUnlocked;
            _price.Value = _planet.Price;

            _planet.OnUnlocked += OnUnlocked;
        }

        public void Dispose()
        {
            _planet.OnUnlocked -= OnUnlocked;
        }
    }
}
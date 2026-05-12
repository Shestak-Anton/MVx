using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPresenter : IPresenter
    {
        private readonly ReactiveProperty<bool> _isUnlocked = new();
        public Observable<bool> IsUnlocked => _isUnlocked;

        private readonly ReactiveProperty<Sprite> _sprite = new();
        public Observable<Sprite> Sprite => _sprite;

        private readonly ReactiveProperty<int> _price = new();
        public Observable<string> Price => _price.Select(it => $"{it}");

        private readonly ReactiveProperty<TimeProgressPresenter> _timeProgressPresenter = new();
        public Observable<TimeProgressPresenter> TimeProgressPresenter => _timeProgressPresenter;

        private readonly ReactiveProperty<CoinPresenter> _coinPresenter = new();
        public Observable<CoinPresenter> CoinPresenter => _coinPresenter;

        private readonly IPlanet _planet;
        private readonly PlanetPresenterFactory<TimeProgressPresenter> _timeProgressPresenterFactory;
        private readonly PlanetPresenterFactory<CoinPresenter> _coinPresenterFactory;
        private readonly PlanetPopupPresenter _popupPresenter;
        
        public IPlanet Planet => _planet;

        [Inject]
        public PlanetPresenter(
            IPlanet planet,
            PlanetPresenterFactory<TimeProgressPresenter> timeProgressPresenterFactory,
            PlanetPopupPresenter popupPresenter,
            PlanetPresenterFactory<CoinPresenter> coinPresenterFactory)
        {
            _planet = planet;
            _timeProgressPresenterFactory = timeProgressPresenterFactory;
            _popupPresenter = popupPresenter;
            _coinPresenterFactory = coinPresenterFactory;
        }

        public void OnLongPress()
        {
            if (!_planet.IsUnlocked) return;
            _popupPresenter.Show(_planet);
        }

        public void OnShortPress()
        {
            if (_planet.IsUnlocked)
            {
                if (_planet.IsIncomeReady)
                {
                    _planet.GatherIncome();
                }
            }
            else
            {
                if (!_planet.CanUnlock) return;
                _planet.Unlock();
            }
        }

        public void Initialize()
        {
            _timeProgressPresenter.Value = _timeProgressPresenterFactory.Create(_planet);
            _coinPresenter.Value = _coinPresenterFactory.Create(_planet);

            _sprite.Value = _planet.GetIcon(_planet.IsUnlocked);
            _isUnlocked.Value = _planet.IsUnlocked;
            _price.Value = _planet.Price;

            _planet.OnUnlocked += OnUnlocked;
        }

        public void Dispose()
        {
            _planet.OnUnlocked -= OnUnlocked;
        }

        private void OnUnlocked()
        {
            _sprite.Value = _planet.GetIcon(true);
            _isUnlocked.Value = true;
        }
    }
}
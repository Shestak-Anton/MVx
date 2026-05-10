using System;
using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPresenter : IInitializable ,IDisposable
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
        
        private readonly IPlanet _planet;

        [Inject]
        public PlanetPresenter(IPlanet planet)
        {
            _planet = planet;
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
            _sprite.Value = _planet.GetIcon(_planet.IsUnlocked);
            _isUnlocked.Value = _planet.IsUnlocked;
            _price.Value = _planet.Price;
            
            _planet.OnUnlocked += OnUnlocked;
        }

        public void Dispose()
        {
            _planet.OnUnlocked -= OnUnlocked;
        }
        
        public class Factory : PlaceholderFactory<IPlanet, PlanetPresenter>
        {
            private readonly DisposableManager _disposableManager;

            [Inject]
            public Factory(DisposableManager disposableManager)
            {
                _disposableManager = disposableManager;
            }


            public override PlanetPresenter Create(IPlanet param)
            {
                var presenter =  base.Create(param);
                presenter.Initialize();
                _disposableManager.Add(presenter);
                return presenter;
            }
        }
        
    }
}
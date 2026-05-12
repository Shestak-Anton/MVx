using System;
using Modules.Planets;
using R3;
using UnityEngine;

namespace Game.Presenters
{
    public sealed class PlanetPopupPresenter
    {
        public Observable<bool> IsVisible => _isVisible;
        private readonly ReactiveProperty<bool> _isVisible = new(false);

        public Observable<bool> IsMaxLevelReached => _isMaxLevelReached;
        private readonly ReactiveProperty<bool> _isMaxLevelReached = new(false);

        public Observable<string> Title => _title;
        private readonly ReactiveProperty<string> _title = new();

        public Observable<string> Population => _population;
        private readonly ReactiveProperty<string> _population = new();

        public Observable<string> Income => _income;
        private readonly ReactiveProperty<string> _income = new();

        public Observable<string> Level => _level;
        private readonly ReactiveProperty<string> _level = new();

        public Observable<string> Cost => _cost;
        private readonly ReactiveProperty<string> _cost = new();

        public Observable<Sprite> Icon => _icon;
        private readonly ReactiveProperty<Sprite> _icon = new();

        private IPlanet _planet;

        public void Hide()
        {
            _isVisible.Value = false;
            _planet = null;
        }

        public void Show(IPlanet planet)
        {
            _planet = planet;
            InvalidateData();
            _isVisible.Value = true;
        }

        public void Upgrade()
        {
            if (_planet.Upgrade())
            {
                InvalidateData();
            }
        }

        private void InvalidateData()
        {
            _title.Value = _planet.Name;
            _icon.Value = _planet.GetIcon(_planet.IsUnlocked);
            _population.Value = $"Population: {_planet.Population}";
            _level.Value = $"Level: {_planet.Level}/{_planet.MaxLevel}";
            _income.Value = $"Income: {Math.Round(_planet.MinuteIncome / 60f)} / sec";
            _cost.Value = _planet.Price.ToString();
            _isMaxLevelReached.Value = _planet.IsMaxLevel;
        }
    }
}
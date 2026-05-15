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
            DisposeSubscriptions();
            _isVisible.Value = false;
            _planet = null;
        }

        public void Show(IPlanet planet)
        {
            _planet = planet;
            InitData();
            _isVisible.Value = true;
            InitSubscriptions();
        }

        public void Upgrade() => _planet.Upgrade();

        private void InitSubscriptions()
        {
            _planet.OnPopulationChanged += OnPopulationChanged;
            _planet.OnIncomeChanged += OnIncomeChanged;
            _planet.OnUpgraded += OnLevelChanged;
        }

        private void DisposeSubscriptions()
        {
            _planet.OnPopulationChanged -= OnPopulationChanged;
            _planet.OnIncomeChanged -= OnIncomeChanged;
            _planet.OnUpgraded -= OnLevelChanged;
        }

        private void OnPopulationChanged(int population) => _population.Value = $"Population: {population}";

        private void OnIncomeChanged(int income) => _income.Value = $"Income: {income}$";

        private void OnLevelChanged(int level)
        {
            _level.Value = $"Level: {level}/{_planet.MaxLevel}";
            _cost.Value = _planet.Price.ToString();
            _isMaxLevelReached.Value = _planet.IsMaxLevel;
        }


        private void InitData()
        {
            _title.Value = _planet.Name;
            _icon.Value = _planet.GetIcon(_planet.IsUnlocked);
            _level.Value = $"Level: {_planet.Level}/{_planet.MaxLevel}";
            _cost.Value = _planet.Price.ToString();
            _isMaxLevelReached.Value = _planet.IsMaxLevel;
            _income.Value = $"Income: {_planet.MinuteIncome}$";
            _population.Value = $"Population: {_planet.Population}";
        }
    }
}
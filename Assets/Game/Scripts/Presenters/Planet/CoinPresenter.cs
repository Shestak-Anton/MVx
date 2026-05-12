using Modules.Planets;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class CoinPresenter : IPresenter
    {
        private readonly ReactiveProperty<bool> _isActive = new();
        public Observable<bool> IsActive => _isActive;

        private readonly IPlanet _planet;

        [Inject]
        public CoinPresenter(IPlanet planet)
        {
            _planet = planet;
        }

        public void Initialize()
        {
            _planet.OnIncomeReady += OnIncomeReady;
        }

        public void Dispose()
        {
            _planet.OnIncomeReady -= OnIncomeReady;
        }
        
        private void OnIncomeReady(bool isReady) => _isActive.Value = isReady;
    }
}
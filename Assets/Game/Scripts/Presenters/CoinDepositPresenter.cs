using System;
using Modules.Money;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class CoinDepositPresenter : IInitializable, IDisposable
    {
        public Observable<(int from, int to)> Money => _money;
        private readonly ReactiveProperty<(int from, int to)> _money = new();

        private readonly IMoneyStorage _moneyStorage;

        [Inject]
        public CoinDepositPresenter(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        void IInitializable.Initialize()
        {
            _money.Value = (_moneyStorage.Money, _moneyStorage.Money);
            _moneyStorage.OnMoneyChanged += OnSpend;
        }

        void IDisposable.Dispose()
        {
            _moneyStorage.OnMoneyChanged -= OnSpend;
        }

        private void OnSpend(int newValue, int previous) => _money.Value = (previous, newValue);
    }
}
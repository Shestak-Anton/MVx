using System;
using Modules.Money;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class CoinDepositPresenter : IInitializable, IDisposable
    {
        public Observable<int> Money => _money;
        private readonly ReactiveProperty<int> _money = new();

        private readonly IMoneyStorage _moneyStorage;

        [Inject]
        public CoinDepositPresenter(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        void IInitializable.Initialize()
        {
            _money.Value = _moneyStorage.Money;
            _moneyStorage.OnMoneySpent += OnSpend;
        }

        void IDisposable.Dispose()
        {
        }

        private void OnSpend(int newValue, int range)
        {
            _money.Value = newValue;
        }
    }
}
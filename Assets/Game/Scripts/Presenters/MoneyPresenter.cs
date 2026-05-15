using System;
using DG.Tweening;
using Modules.Money;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class MoneyPresenter : IInitializable, IDisposable
    {
        public Observable<string> Money => _money.Select(it => it.ToString());
        public Observable<Unit> OnMoneyEarned => _onEarnedCommand;
        public Observable<Unit> OnMoneySpent => _onSpentCommand;

        private readonly ReactiveProperty<int> _money = new();
        private readonly ReactiveCommand _onEarnedCommand = new();
        private readonly ReactiveCommand _onSpentCommand = new();

        private readonly IMoneyStorage _moneyStorage;

        [Inject]
        public MoneyPresenter(IMoneyStorage moneyStorage) => _moneyStorage = moneyStorage;

        void IInitializable.Initialize()
        {
            _money.Value = _moneyStorage.Money;
            _moneyStorage.OnMoneyChanged += OnChange;
            _moneyStorage.OnMoneyEarned += OnEarned;
            _moneyStorage.OnMoneySpent += OnSpent;
        }

        void IDisposable.Dispose()
        {
            _moneyStorage.OnMoneyChanged -= OnChange;
            _moneyStorage.OnMoneyEarned -= OnEarned;
            _moneyStorage.OnMoneySpent -= OnSpent;
        }

        private void OnSpent(int newValue, int range) => _onSpentCommand.Execute(Unit.Default);

        private void OnEarned(int newValue, int range) => _onEarnedCommand.Execute(Unit.Default);

        private void OnChange(int current, int from)
        {
            const string counterId = "CounterTextView.counter";
            DOTween.Kill(counterId, true);

            var newValue = from;
            DOTween.To(
                getter: () => newValue,
                setter: value =>
                {
                    newValue = value;
                    _money.Value = newValue;
                },
                endValue: current,
                duration: 0.3f
            ).SetId(counterId);
        }
    }
}
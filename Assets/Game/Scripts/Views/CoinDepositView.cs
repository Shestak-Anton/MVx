using Game.Presenters;
using Modules.UI;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class CoinDepositView : MonoBehaviour
    {
        [SerializeField] private CounterTextView _textView;

        private CoinDepositPresenter _coinDepositPresenter;

        [Inject]
        public void Construct(CoinDepositPresenter coinDepositPresenter)
        {
            _coinDepositPresenter = coinDepositPresenter;
        }

        private void Awake()
        {
            var money = _coinDepositPresenter.Money;
            money
                .Skip(1)
                .Subscribe((tuple) => _textView.Animate(tuple.from, tuple.to))
                .AddTo(this);
            money
                .Take(1)
                .Subscribe((tuple) => _textView.Init(tuple.to))
                .AddTo(this);
        }
    }
}
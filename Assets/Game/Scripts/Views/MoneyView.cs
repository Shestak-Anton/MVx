using Game.Presenters;
using Modules.UI;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class MoneyView : MonoBehaviour
    {
        [SerializeField] private CounterTextView _textView;

        [Inject]
        public void Construct(MoneyPresenter moneyPresenter)
        {
            moneyPresenter.Money
                .Subscribe(it => _textView.SetText(it))
                .AddTo(this);
            moneyPresenter.OnMoneyEarned
                .Subscribe(it => _textView.AnimateEarned())
                .AddTo(this);
            moneyPresenter.OnMoneySpent
                .Subscribe(it => _textView.AnimateSpent())
                .AddTo(this);
        }
    }
}
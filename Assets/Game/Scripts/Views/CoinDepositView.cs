using System;
using Game.Presenters;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class CoinDepositView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;

        private CoinDepositPresenter _coinDepositPresenter;

        [Inject]
        public void Construct(CoinDepositPresenter coinDepositPresenter)
        {
            _coinDepositPresenter = coinDepositPresenter;
        }

        private void Awake()
        {
            _coinDepositPresenter.Money
                .Subscribe(it => _money.text = it.ToString())
                .AddTo(this);
        }
    }
}
using System;
using Game.Presenters;
using Modules.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public sealed class PlanetView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _coin;
        [SerializeField] private Image _progressBar;
        [SerializeField] private GameObject _income;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private GameObject _priceContainter;
        [SerializeField] private SmartButton _smartButton;

        private PlanetPresenter _planetPresenter;

        public void Init(PlanetPresenter planetPresenter)
        {
            _planetPresenter = planetPresenter;
            if (_planetPresenter == null) return;

            _smartButton.OnClick += _planetPresenter.OnShortPress;
            _smartButton.OnHold += _planetPresenter.OnLongPress;

            _planetPresenter.Sprite
                .Subscribe(sprite => _icon.sprite = sprite)
                .AddTo(this);
            _planetPresenter.IsUnlocked
                .Subscribe(SetIsUnlockState)
                .AddTo(this);
            _planetPresenter.Price
                .Subscribe(price => _price.text = $"{price}")
                .AddTo(this);
        }

        private void OnDestroy()
        {
            if (_planetPresenter == null) return;

            _smartButton.OnClick -= _planetPresenter.OnShortPress;
            _smartButton.OnHold -= _planetPresenter.OnLongPress;
        }

        private void SetIsUnlockState(bool isUnlocked)
        {
            _lock.gameObject.SetActive(!isUnlocked);
            _priceContainter.SetActive(!isUnlocked);
            _income.SetActive(isUnlocked);
        }
    }
}
using Game.Presenters;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public sealed class PlanetPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _population;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private GameObject _popup;

        private PlanetPopupPresenter _planetPopupPresenter;

        [Inject]
        public void Construct(PlanetPopupPresenter presenter)
        {
            _planetPopupPresenter = presenter;
        }

        private void Awake()
        {
            _closeButton.OnClickAsObservable()
                .Subscribe(_ => _planetPopupPresenter.Hide())
                .AddTo(this);
            _upgradeButton.OnClickAsObservable()
                .Subscribe(_ => _planetPopupPresenter.Upgrade())
                .AddTo(this);

            _planetPopupPresenter.IsMaxLevelReached
                .Subscribe(it => _upgradeButton.gameObject.SetActive(!it))
                .AddTo(this);
            _planetPopupPresenter.Title
                .Subscribe(it => _title.text = it)
                .AddTo(this);
            _planetPopupPresenter.Population
                .Subscribe(it => _population.text = it)
                .AddTo(this);
            _planetPopupPresenter.Income
                .Subscribe(it => _income.text = it)
                .AddTo(this);
            _planetPopupPresenter.Level
                .Subscribe(it => _level.text = it)
                .AddTo(this);
            _planetPopupPresenter.Cost
                .Subscribe(it => _price.text = it)
                .AddTo(this);
            _planetPopupPresenter.Icon
                .Subscribe(it => _icon.sprite = it)
                .AddTo(this);
            
            _planetPopupPresenter.IsVisible
                .Subscribe(SetIsVisible)
                .AddTo(this);
        }

        private void SetIsVisible(bool isVisible) => _popup.SetActive(isVisible);
        
    }
}
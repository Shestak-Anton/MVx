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
            
            _planetPopupPresenter.IsVisible
                .Subscribe(SetIsVisible)
                .AddTo(this);
        }

        private void SetIsVisible(bool isVisible) => _popup.SetActive(isVisible);
        
    }
}
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
        [SerializeField] private TextMeshProUGUI _populationText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _upgradeButton;

        private PlanetPopupPresenter _planetPopupPresenter;

        [Inject]
        public void Construct(PlanetPopupPresenter presenter)
        {
            _planetPopupPresenter = presenter;
        }

        private void Awake()
        {
            _closeButton.OnClickAsObservable()
                .Subscribe(_ => _planetPopupPresenter.OnCloseClicked())
                .AddTo(this);
            _upgradeButton.OnClickAsObservable()
                .Subscribe(_ => _planetPopupPresenter.OnUpgradeClicked())
                .AddTo(this);
            
            _planetPopupPresenter.IsVisible
                .Subscribe(SetIsVisible)
                .AddTo(this);
        }

        private void SetIsVisible(bool isVisible) => gameObject.SetActive(isVisible);
        
    }
}
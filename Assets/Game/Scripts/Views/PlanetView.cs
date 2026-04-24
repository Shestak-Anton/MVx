using Game.Presenters;
using Modules.UI;
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
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private SmartButton _smartButton;

        private PlanetPresenter _planetPresenter;

        [Inject]
        public void Construct(PlanetPresenter planetPresenter)
        {
            _planetPresenter = planetPresenter;
        }

        private void Awake()
        {
            _smartButton.OnClick += _planetPresenter.OnShortPress;
            _smartButton.OnHold += _planetPresenter.OnLongPress;
        }

        private void OnDestroy()
        {
            _smartButton.OnClick -= _planetPresenter.OnShortPress;
            _smartButton.OnHold -= _planetPresenter.OnLongPress;
        }
    }
}
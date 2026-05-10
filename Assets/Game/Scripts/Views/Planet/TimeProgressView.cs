using Game.Presenters;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public sealed class TimeProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _time;

        public void Init(TimeProgressPresenter presenter)
        {
            presenter.Progress
                .Subscribe(it => _progressBar.fillAmount = it)
                .AddTo(this);
            presenter.Time
                .Subscribe(it => _time.text = it)
                .AddTo(this);
        }
    }
}
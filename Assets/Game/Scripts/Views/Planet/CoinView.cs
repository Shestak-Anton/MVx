using Game.Presenters;
using R3;
using UnityEngine;

namespace Game.Views
{
    public sealed class CoinView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Vector3 Position => _rectTransform.position;
        
        public void Init(CoinPresenter presenter)
        {
            presenter.IsActive
                .Subscribe(it => _canvasGroup.alpha = it ? 1f : 0f)
                .AddTo(_rectTransform);
        }
    }
}
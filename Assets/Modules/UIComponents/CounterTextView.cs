using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Modules.UI
{
    public sealed class CounterTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _gainColor = Color.lightGreen;
        [SerializeField] private Color _spendColor = Color.softRed;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = _text.GetComponent<RectTransform>();
        }
        
        public void SetText(string text) => _text.text = text;

        public void AnimateEarned() => Animate(isSpent: false);

        public void AnimateSpent() => Animate(isSpent: true);

        private void Animate(bool isSpent, float delay = 0.0f, float duration = 0.3f)
        {
            const string colorId = "CounterTextView.textColor";
            const string scaleId = "CounterTextView.textScale";

            DOTween.Kill(colorId, true);
            DOTween.Kill(scaleId, true);

            _text.DOColor(isSpent ? _spendColor : _gainColor, duration)
                .SetDelay(delay)
                .SetLoops(2, LoopType.Yoyo)
                .SetId(colorId);

            _rectTransform.DOScale(1.2f, duration)
                .SetDelay(delay)
                .SetLoops(2, LoopType.Yoyo)
                .SetId(scaleId);
        }
    }
}
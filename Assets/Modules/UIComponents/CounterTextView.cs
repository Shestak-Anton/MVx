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

        public void Init(int initial)
        {
            _text.text = initial.ToString();
        }

        public void Animate(int from, int to, float delay = 0.0f, float duration = 0.3f)
        {
            const string colorId = "CounterTextView.textColor";
            const string scaleId = "CounterTextView.textScale";
            const string counterId = "CounterTextView.counter";

            DOTween.Kill(colorId, true);
            DOTween.Kill(scaleId, true);
            DOTween.Kill(counterId, true);

            _text.DOColor(from > to ? _spendColor : _gainColor, duration)
                .SetDelay(delay)
                .SetLoops(2, LoopType.Yoyo)
                .SetId(colorId);

            _rectTransform.DOScale(1.2f, duration)
                .SetDelay(delay)
                .SetLoops(2, LoopType.Yoyo)
                .SetId(scaleId);

            var newValue = from;
            DOTween.To(
                    getter: () => newValue,
                    setter: value =>
                    {
                        newValue = value;
                        _text.text = newValue.ToString();
                    },
                    endValue: to,
                    duration
                )
                .SetDelay(delay)
                .SetId(counterId);
        }
    }
}
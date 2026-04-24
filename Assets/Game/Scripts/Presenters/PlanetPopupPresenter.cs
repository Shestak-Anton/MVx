using R3;

namespace Game.Presenters
{
    public sealed class PlanetPopupPresenter
    {
        public ReadOnlyReactiveProperty<bool> IsVisible => _isVisible;
        private readonly ReactiveProperty<bool> _isVisible = new(false);
        
        public void Hide()
        {
            _isVisible.Value = false;
        }

        public void Show()
        {
            _isVisible.Value = true;
        }

        public void Upgrade()
        {
        }
    }
}
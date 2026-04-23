using R3;

namespace Game.Presenters
{
    public sealed class PlanetPopupPresenter
    {
        public ReadOnlyReactiveProperty<bool> IsVisible => _isVisible;
        private readonly ReactiveProperty<bool> _isVisible = new(false);

        public void OnCloseClicked()
        {
            _isVisible.Value = false;
        }
        
        public void OnUpgradeClicked(){}
        
    }
}
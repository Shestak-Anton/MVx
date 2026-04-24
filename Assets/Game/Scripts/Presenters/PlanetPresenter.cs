using System;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly PlanetPopupPresenter _planetPopupPresenter;


        [Inject]
        public PlanetPresenter(PlanetPopupPresenter planetPopupPresenter)
        {
            _planetPopupPresenter = planetPopupPresenter;
        }

        void IInitializable.Initialize()
        {
        }

        void IDisposable.Dispose()
        {
        }

        public void OnLongPress()
        {
            _planetPopupPresenter.Show();
        }

        public void OnShortPress()
        {
        }
    }
}
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenterFactory<T> : PlaceholderFactory<IPlanet, T> where T : IPresenter
    {
        private readonly DiContainer _container;

        [Inject]
        public PlanetPresenterFactory(DiContainer container)
        {
            _container = container;
        }


        public override T Create(IPlanet param)
        {
            var presenter = base.Create(param);
            presenter.Initialize();
            _container.Resolve<DisposableManager>().Add(presenter);
            return presenter;
        }
    }
}
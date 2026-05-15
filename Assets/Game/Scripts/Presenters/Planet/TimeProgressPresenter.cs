using System;
using Modules.Planets;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class TimeProgressPresenter : IPresenter
    {
        private readonly ReactiveProperty<float> _progress = new();
        public Observable<float> Progress => _progress.Select(_ => _planet.IncomeProgress);
        public Observable<string> Time => _progress.Select(FormatTime);

        private readonly ReactiveProperty<bool> _isIncomeReady = new();
        public Observable<bool> IsIncomeReady => _isIncomeReady.Skip(1);

        private readonly IPlanet _planet;

        [Inject]
        public TimeProgressPresenter(IPlanet planet)
        {
            _planet = planet;
        }

        void IInitializable.Initialize()
        {
            _planet.OnIncomeTimeChanged += OnTimeChanged;
            _planet.OnIncomeReady += OnIncomeReady;
        }

        void IDisposable.Dispose()
        {
            _planet.OnIncomeTimeChanged -= OnTimeChanged;
            _planet.OnIncomeReady -= OnIncomeReady;
        }

        private void OnTimeChanged(float time) => _progress.Value = time;

        private void OnIncomeReady(bool isReady) => _isIncomeReady.Value = isReady;

        private static string FormatTime(float seconds) =>
            TimeSpan.FromSeconds(seconds + 1f).ToString(@"mm\:ss");
    }
}
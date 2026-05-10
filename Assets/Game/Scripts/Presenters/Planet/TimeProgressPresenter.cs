using System;
using Modules.Planets;
using R3;
using Zenject;

namespace Game.Presenters
{
    public sealed class TimeProgressPresenter : IPresenter
    {
        private readonly ReactiveProperty<float> _progress = new();
        public Observable<float> Progress => _progress.Select(_ => 1f - _planet.IncomeProgress);
        public Observable<string> Time => _progress.Select(FormatTime);

        private readonly IPlanet _planet;

        public TimeProgressPresenter(IPlanet planet)
        {
            _planet = planet;
        }

        void IInitializable.Initialize()
        {
            _planet.OnIncomeTimeChanged += OnTimeChanged;
        }

        void IDisposable.Dispose()
        {
            _planet.OnIncomeTimeChanged -= OnTimeChanged;
        }

        private void OnTimeChanged(float time) => _progress.Value = time;

        private static string FormatTime(float seconds) =>
            TimeSpan.FromSeconds(seconds + 1f).ToString(@"mm\:ss");
    }
}
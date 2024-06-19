using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    public abstract class Animation
    {
        private readonly Stopwatch _stopWatch = new();

        public void RestartTimer() => _stopWatch.Restart();

        public void StopTimer() => _stopWatch.Stop();

        public abstract void ChangeFrame();

        public abstract DisplayedSection AsDisplayedSection();

        public bool IsReadyToUpdate => _stopWatch.ElapsedMilliseconds > RefreshRateInMilliseconds;

        public abstract long RefreshRateInMilliseconds { get; }
    }
}

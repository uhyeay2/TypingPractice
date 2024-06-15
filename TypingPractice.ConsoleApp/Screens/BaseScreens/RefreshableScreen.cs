using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class RefreshableScreen : Screen
    {
        private long _refreshCount;

        protected long RefreshCount => _refreshCount;

        public abstract DisplayedSection GetDisplay();

        public virtual void RefreshUntilKeyAvailable()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            while (!Console.KeyAvailable)
            {
                if (stopWatch.ElapsedMilliseconds > RefreshRatioInMilliseconds)
                {
                    TriggerRefresh();

                    Console.SetCursorPosition(0, 0);

                    Print(GetDisplay());

                    stopWatch.Restart();
                }
            }
        }

        public abstract long RefreshRatioInMilliseconds { get; }

        public virtual void TriggerRefresh() => _refreshCount = _refreshCount == long.MaxValue ? 0 : _refreshCount + 1;       
    }
}

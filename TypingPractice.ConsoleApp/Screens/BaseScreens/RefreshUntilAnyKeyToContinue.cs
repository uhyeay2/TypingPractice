using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class RefreshUntilAnyKeyToContinue : AnyKeyToContinue
    {
        public abstract long RefreshRatioInMilliseconds { get; }

        private long _refreshCount;

        protected long RefreshCount => _refreshCount;

        public virtual void TriggerRefresh() => _refreshCount = _refreshCount == long.MaxValue ? 0 : _refreshCount + 1;

        public override Screen DisplayScreenAndGetNext()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            new DisplayedScreen(GetDisplay()).ClearThenWrite();
            
            while (!Console.KeyAvailable)
            {
                if (stopWatch.ElapsedMilliseconds > RefreshRatioInMilliseconds)
                {
                    TriggerRefresh();

                    Console.SetCursorPosition(0, 0);

                    new DisplayedScreen(GetDisplay()).Write();

                    stopWatch.Restart();
                }
            }

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class RefreshableScreen : Screen
    {
        private long _refreshCount;

        protected long RefreshCount => _refreshCount;

        private readonly Stopwatch RefreshStopWatch = new();

        public abstract DisplayedSection GetDisplay();

        //TODO: Refactor so that all animations are using this
        public virtual IEnumerable<Animation> GetAnimations() => [];

        public virtual void RefreshUntilKeyAvailable() => RefreshUntilKeyAvailable(GetDisplay);

        public virtual void RefreshUntilKeyAvailable(Func<DisplayedSection> funcGetDisplay)
        {
            RefreshStopWatch.Restart();

            foreach (var animation in GetAnimations())
            {
                animation.RestartTimer();
            }

            while (!Console.KeyAvailable)
            {
                foreach (var animation in GetAnimations())
                {
                    if (animation.IsReadyToUpdate)
                    {
                        animation.ChangeFrame();

                        animation.RestartTimer();
                    }
                }

                //TODO: Replace previous animations with new Animation object
                if (RefreshStopWatch.ElapsedMilliseconds >= RefreshRatioInMilliseconds)
                {
                    TriggerRefresh();

                    Console.SetCursorPosition(0, 0);

                    Print(funcGetDisplay.Invoke());

                    RefreshStopWatch.Restart();
                }
            }

            foreach (var animation in GetAnimations())
            {
                animation.StopTimer();
            }
        }

        public abstract long RefreshRatioInMilliseconds { get; }

        public virtual void TriggerRefresh() => _refreshCount = _refreshCount == long.MaxValue ? 0 : _refreshCount + 1;       
    }
}

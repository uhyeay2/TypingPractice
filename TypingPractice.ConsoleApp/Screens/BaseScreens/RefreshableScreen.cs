using System.Diagnostics;
using TypingPractice.ConsoleApp.Constants;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Exceptions;
using TypingPractice.ConsoleApp.Screens.Settings;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class RefreshableScreen : Screen
    {
        private long _refreshCount;

        protected long RefreshCount => _refreshCount;

        private readonly Stopwatch RefreshStopWatch = new();

        private int _lastWidth = Console.WindowWidth;
        
        private int _lastHeight = Console.WindowHeight;

        public abstract DisplayedSection GetDisplay();

        //TODO: Refactor so that all animations are using this
        public virtual IEnumerable<Animation> GetAnimations() => [];

        public virtual void RefreshUntilKeyAvailable() => RefreshUntilKeyAvailable(GetDisplay);

        public virtual void RefreshUntilKeyAvailable(Func<DisplayedSection> funcGetDisplay, Func<bool>? funcForceStop = null)
        {
            RefreshStopWatch.Restart();

            foreach (var animation in GetAnimations())
            {
                if (animation.IsReadyToUpdate)
                {
                    animation.ChangeAnimation();

                    animation.RestartTimer();
                }
                else
                {
                    animation.RestartTimer();
                }
            }

            funcForceStop ??= () => false;

            while (!Console.KeyAvailable && !funcForceStop.Invoke())
            {
                if (!ConsoleSize.IsMeetingMinimumRequirements && this is not UpdateConsoleSizeScreen)
                {
                    throw new ConsoleSizeTooSmallException(this);
                }

                // Clear screen if it has been resized
                if (_lastHeight != Console.WindowHeight || _lastWidth != Console.WindowWidth)
                {
                    Console.Clear();
                    _lastHeight = Console.WindowHeight;
                    _lastWidth = Console.WindowWidth;
                    Console.CursorVisible = false;
                }

                // Update any of the animations that need are ready to change frames
                foreach (var animation in GetAnimations())
                {
                    if (animation.IsReadyToUpdate)
                    {
                        animation.ChangeAnimation();

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

        public virtual long RefreshRatioInMilliseconds { get; } = 50;

        public virtual void TriggerRefresh() => _refreshCount = _refreshCount == long.MaxValue ? 0 : _refreshCount + 1;       
    }
}

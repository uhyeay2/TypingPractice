using System.Diagnostics;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.SplashScreens;
using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ExerciseScreen : Screen
    {
        public abstract PauseScreen GetPauseScreen();

        public abstract long RefreshRatioInMilliseconds { get; }
        
        private long _refreshCount;

        protected long RefreshCount => _refreshCount;

        public virtual void TriggerRefresh() => _refreshCount = _refreshCount == long.MaxValue ? 0 : _refreshCount + 1;

        protected string ElapsedExerciseTime => $"{ExerciseStopwatch.Elapsed:hh}:{ExerciseStopwatch.Elapsed:mm}:{ExerciseStopwatch.Elapsed:ss}.{ExerciseStopwatch.Elapsed:ff}";

        public virtual void StartExercise()
        {
            KeepTyping = true;

            Print(GetDisplayBeforeExercise(), clearScreen: true);

            Console.ReadKey(true);

            ExerciseStopwatch.Start();
        }

        public abstract void StartExerciseLoop();

        public abstract DisplayedSection GetDisplayDuringExercise();

        public abstract DisplayedSection GetDisplayBeforeExercise();

        public virtual void StopExercise()
        {
            ExpectedCharacter = null;
            LastKeyTyped = null;
            ExerciseStopwatch.Stop();
        }

        protected List<KeyTypedStat> KeysTyped = [];

        protected KeyTypedStat? LastKeyTyped;

        protected char? ExpectedCharacter;

        protected Stopwatch ExerciseStopwatch = new();
        
        protected Stopwatch KeyStrokeStopwatch = new();

        protected bool KeepTyping = true;

        public virtual void CaptureInput()
        {
            KeyStrokeStopwatch.Restart();

            var refreshingStopWatch = new Stopwatch();

            refreshingStopWatch.Start();

            while (!Console.KeyAvailable)
            {
                if (refreshingStopWatch.ElapsedMilliseconds >= RefreshRatioInMilliseconds)
                {
                    TriggerRefresh();

                    Console.SetCursorPosition(0, 0);

                    Print(GetDisplayDuringExercise());

                    refreshingStopWatch.Restart();
                }
            }

            var input = Console.ReadKey(true);

            var milliseconds = KeyStrokeStopwatch.Elapsed.TotalMilliseconds;

            if (input.Key == ConsoleKey.Escape || input.Key == ConsoleKey.Enter)
            {
                KeepTyping = false;
            }
            else
            {
                LastKeyTyped = new(LastKeyTyped, milliseconds, input.KeyChar, ExpectedCharacter.GetValueOrDefault());

                KeysTyped.Add(LastKeyTyped);
            }
        }

        public override Screen DisplayScreenAndGetNext()
        {
            StartExercise();

            while (KeepTyping)
            {
                Console.SetCursorPosition(0, 0);

                StartExerciseLoop();

                Print(GetDisplayDuringExercise());

                CaptureInput();
            }

            StopExercise();

            return GetPauseScreen();
        }
    }
}

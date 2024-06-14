using System.Diagnostics;
using TypingPractice.ConsoleApp.Models;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ExerciseScreen : RefreshableScreen
    {
        protected bool KeepTyping = true;

        public override long RefreshRatioInMilliseconds { get; } = 5;

        protected List<KeyTypedStat> KeysTyped = [];

        protected KeyTypedStat? LastKeyTyped;

        protected char? ExpectedCharacter;

        protected Stopwatch KeyStrokeStopwatch = new();

        protected Stopwatch ExerciseStopwatch = new();

        protected string ElapsedExerciseTime => $"{ExerciseStopwatch.Elapsed:hh}:{ExerciseStopwatch.Elapsed:mm}:{ExerciseStopwatch.Elapsed:ss}.{ExerciseStopwatch.Elapsed:ff}";

        public abstract void StartExerciseLoop();

        public override Screen DisplayScreenAndGetNext()
        {
            StartExercise();

            while (KeepTyping)
            {
                Console.SetCursorPosition(0, 0);

                StartExerciseLoop();

                Print(GetDisplay());

                RefreshUntilKeyAvailable();

                ProcessInput(Console.ReadKey(true));
            }

            StopExercise();

            return GetNextScreen();
        }

        public virtual void StartExercise()
        {
            KeepTyping = true;

            Print(GetDisplay(), clearScreen: true);

            Console.ReadKey(true);

            ExerciseStopwatch.Start();
        }

        public override void RefreshUntilKeyAvailable()
        {
            KeyStrokeStopwatch.Restart();

            base.RefreshUntilKeyAvailable();
        }

        public virtual void ProcessInput(ConsoleKeyInfo input)
        {
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

        public virtual void StopExercise()
        {
            ExpectedCharacter = null;
            LastKeyTyped = null;
            ExerciseStopwatch.Stop();
        }
    }
}

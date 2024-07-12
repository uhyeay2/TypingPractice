using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Exercises
{
    public abstract class BaseExercise: RefreshableScreen
    {
        #region Private Members

        private ExerciseState _exerciseState = ExerciseState.NotStarted;

        private readonly Stopwatch _exerciseStopwatch = new();

        private readonly Stopwatch _keyStrokeStopwatch = new();

        private char? _expectedInput;

        private KeyTypedStat? _lastKeyTyped;

        private readonly List<KeyTypedStat> _keysTyped = [];

        private int _currentLives;

        #endregion

        #region Constructor

        public BaseExercise(ExerciseSettings settings)
        {
            _settings = settings;

            _currentLives = _settings.StartingLives;            
        }

        #endregion

        #region Protected Members

        protected readonly ExerciseSettings _settings;

        protected bool IsGameOver() => IsExerciseOverDueToExceedingTimeLimit() || IsExerciseOverDueToMinimumKeysPerSecond() || IsExerciseOverDueToMinimumWordsPerMinute();

        protected bool IsExerciseOverDueToExceedingTimeLimit() =>
            _settings.ExerciseTimeLimitInSeconds != null
            && _exerciseStopwatch.Elapsed.TotalSeconds >= _settings.ExerciseTimeLimitInSeconds;

        protected bool IsExerciseOverDueToMinimumKeysPerSecond() =>
            _settings.MinimumKeysPerSecond != null && _keysTyped.Count > 5 && GetKeysPerMinute() < _settings.MinimumKeysPerSecond;

        protected bool IsExerciseOverDueToMinimumWordsPerMinute() =>
            _settings.MinimumWordsPerMinute != null && _keysTyped.Count > 5 && GetWordsPerMinute() < _settings.MinimumWordsPerMinute;

        protected bool IsExerciseOverDueToIncorrectInput(bool isLastKeyTypedCorrect) =>
            isLastKeyTypedCorrect || _settings.StartingLives <= 0 ? false : --_currentLives <= 0;

        /// <summary>
        /// Returns the count of keys typed divided by the ExerciseStopWatch.Elapsed.TotalSeconds.
        /// If KeysTyped.Count is 0 or ExerciseStopWatch.Elapsed.Seconds is less than or equal to 0 then returns NULL.
        /// </summary>
        protected double? GetKeysPerMinute() =>
            (_keysTyped.Count == 0 || _exerciseStopwatch.Elapsed.Seconds <= 0) ? null : _keysTyped.Count / (_exerciseStopwatch.Elapsed.TotalMilliseconds / 1000) * 60;

        /// <summary>
        /// Returns the Keys Per Second multiplied by 60 (seconds) divided by 5 (characters per word) to calculate the Words Per Minute.
        /// If KeysTyped.Count is 0 or ExerciseStopWatch.Elapsed.Seconds is less than or equal to 0 then returns NULL.
        /// </summary>
        protected double? GetWordsPerMinute() =>
            (_keysTyped.Count == 0 || _exerciseStopwatch.Elapsed.Seconds <= 0) ? null : GetKeysPerMinute() / 5;

        public double? GetAccuracy()
        {
            if (_keysTyped.Count == 0)
            {
                return null;
            }

            double correctCount = _keysTyped.Count(_ => _.IsCorrectKeyTyped);

            return (correctCount / _keysTyped.Count) * 100;
        }

        protected char? ExpectedInput => _expectedInput;

        protected int CurrentLives => _currentLives;

        protected KeyTypedStat? LastKeyTyped => _lastKeyTyped;

        protected IReadOnlyList<KeyTypedStat> KeysTyped => _keysTyped;

        protected TimeSpan ElapsedExerciseTime => _exerciseStopwatch.Elapsed;

        protected ConsoleColor InputBasedColor => ExpectedInput == null || LastKeyTyped == null ? SecondaryFontColor
                                                : LastKeyTyped.IsCorrectKeyTyped ? ConsoleColor.Green : ConsoleColor.Red;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Method for returning the display that should be shown before the exercise starts. This is also what would be seen when the exercise is un-paused.
        /// </summary>
        /// <returns>The DisplayedSection to continue refreshing until any key is pressed to start the exercise.</returns>
        public abstract DisplayedSection GetDisplayBeforeStarting();

        /// <summary>
        /// Method for returning the display during the exercise. This is what will continue refreshing while waitng for any key to be pressed during gameplay.
        /// </summary>
        /// <returns>The DisplayedSection to continue refreshing until any key is pressed during gameplay.</returns>
        public abstract DisplayedSection GetDisplayDuringExercise();

        /// <summary>
        /// Called at the beginning of each exercise loop, this is what will set the expected input to be checked for.
        /// </summary>
        public abstract char? GetExpectedInput();

        public abstract Screen GetGameOverScreen();

        public abstract PauseScreen GetPauseScreen();

        public abstract void NotifyExpectedInputGeneratorOfMistake();

        #endregion

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            RefreshUntilKeyAvailable(GetDisplayBeforeStarting);

            Console.ReadKey(true);

            _exerciseState = ExerciseState.Active;

            _exerciseStopwatch.Start();

            try
            {
                while (_exerciseState == ExerciseState.Active)
                {
                    Console.SetCursorPosition(0, 0);

                    _expectedInput = GetExpectedInput();

                    _keyStrokeStopwatch.Restart();

                    RefreshUntilKeyAvailable(GetDisplayDuringExercise, funcForceStop: IsGameOver);

                    if (IsGameOver())
                    {
                        _exerciseState = ExerciseState.GameOver;
                    }
                    else
                    {
                        var input = Console.ReadKey(true);

                        var milliseconds = _keyStrokeStopwatch.Elapsed.TotalMilliseconds;

                        if (input.Key == ConsoleKey.Escape || input.Key == ConsoleKey.Enter)
                        {
                            _exerciseState = ExerciseState.Paused;
                        }
                        else
                        {
                            _lastKeyTyped = new(_lastKeyTyped, milliseconds, input.KeyChar, _expectedInput.GetValueOrDefault());

                            if (!_lastKeyTyped.IsCorrectKeyTyped)
                            {
                                NotifyExpectedInputGeneratorOfMistake();
                            }

                            _keysTyped.Add(_lastKeyTyped);

                            if (IsExerciseOverDueToIncorrectInput(_lastKeyTyped.IsCorrectKeyTyped))
                            {
                                _exerciseState = ExerciseState.GameOver;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Logging
                var stackTrace = e.StackTrace;

                _exerciseState = ExerciseState.Paused;
            }
            finally
            {
                _exerciseStopwatch.Stop();

                _expectedInput = null;

                _lastKeyTyped = null;
            }

            return _exerciseState == ExerciseState.Paused ? GetPauseScreen() : GetGameOverScreen();
        }

        //TODO: Remove this
        public override DisplayedSection GetDisplay()
        {
            throw new NotImplementedException();
        }
    }
}

namespace TypingPractice.ConsoleApp.Exercises
{
    public class ExerciseSettingsBuilder
    {
        #region Private Members For Initializing Exercise Settings

        private bool _isHardCoreEnabled = false;

        private int _startingLives = 0;

        private int _maxLives = 1;

        private int? _exerciseTimeLimitInSeconds = null;

        private double? _minimumKeysPerSecond = null;

        private double? _minimumWordsPerMinute = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize and Return an ExerciseSettings using the settings applied so far.
        /// </summary>
        /// <returns>New instance of ExerciseSettings</returns>
        public ExerciseSettings Build() => 
            new(_isHardCoreEnabled, _startingLives, _maxLives, _exerciseTimeLimitInSeconds, _minimumKeysPerSecond, _minimumWordsPerMinute);

        /// <summary>
        /// Turns HardCoreMode on, sets StartingLives and MaxLives to 1.
        /// </summary>
        /// <returns>The instance of ExerciseSettingsBuilder that is calling this method.</returns>
        public ExerciseSettingsBuilder EnableHardCoreMode()
        {
            _isHardCoreEnabled = true;

            _startingLives = 1;

            _maxLives = 1;

            return this;
        }

        /// <summary>
        /// Turns HardCoreMode off, sets StartingLives and MaxLives to whatever is passed in. 
        /// If either int is less than 1, then it will default to 1.
        /// </summary>
        /// <param name="startingLives">The number of lives to start the exercise with. If less than 1, then will default to 1.</param>
        /// <param name="maxLives">The maximum number of lives that can be obtained during the exercise. If less than 1, then will default to 1.</param>
        /// <returns>The instance of ExerciseSettingsBuilder that is calling this method.</returns>
        public ExerciseSettingsBuilder EnableLivesMode(int startingLives, int maxLives)
        {
            if (startingLives < 1)
            {
                startingLives = 1;
            }

            if (maxLives < 1)
            {
                maxLives = 1;
            }

            _isHardCoreEnabled = false;

            _startingLives = startingLives;

            _maxLives = maxLives;

            return this;
        }

        /// <summary>
        /// Sets the TimeLimit for the Exercise in seconds. Setting to NULL will disable TimeLimit for the exercise.
        /// </summary>
        /// <param name="timeLimitInSeconds">The number of seconds that the Exercise will last for.</param>
        /// <returns>The instance of ExerciseSettingsBuilder that is calling this method.</returns>
        public ExerciseSettingsBuilder SetExerciseTimeLimit(int? timeLimitInSeconds)
        {
            _exerciseTimeLimitInSeconds = timeLimitInSeconds;

            return this;
        }

        /// <summary>
        /// Sets the minimum Keys Per Second that the user must sustain in order to continue playing the Exercise.
        /// </summary>
        /// <param name="minimumKeysPerSecond">The minimum Keys Per Second that the user must sustain in order to continue playing the Exercise.</param>
        /// <returns>The instance of ExerciseSettingsBuilder that is calling this method.</returns>
        public ExerciseSettingsBuilder SetMinimumKeysPerSecond(double? minimumKeysPerSecond) 
        { 
            _minimumKeysPerSecond = minimumKeysPerSecond;

            return this;
        }

        /// <summary>
        /// Sets the minimum Words Per Minute that the user must sustain in order to continue playing the Exercise.
        /// </summary>
        /// <param name="minimumWordsPerMinute">The minimum Words Per Minute that the user must sustain in order to continue playing the Exercise.</param>
        /// <returns>The instance of ExerciseSettingsBuilder that is calling this method.</returns>
        public ExerciseSettingsBuilder SetMinimumWordsPerMinute(double? minimumWordsPerMinute)
        {
            _minimumWordsPerMinute = minimumWordsPerMinute;

            return this;
        }

        #endregion
    }
}

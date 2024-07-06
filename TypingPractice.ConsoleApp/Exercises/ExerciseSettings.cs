namespace TypingPractice.ConsoleApp.Exercises
{
    public class ExerciseSettings
    {
        #region Constructor

        public ExerciseSettings(bool isHardCoreModeEnabled, int startingLives, int maxLives, int? exerciseTimeLimitInSeconds, double? minimumKeysPerSecond, double? minimumWordsPerMinute)
        {
            _isHardCoreModeEnabled = isHardCoreModeEnabled;

            _startingLives = startingLives;

            _maxLives = maxLives;

            _exerciseTimeLimitInSeconds = exerciseTimeLimitInSeconds;

            _minimumKeysPerSecond = minimumKeysPerSecond;

            _minimumWordsPerMinute = minimumWordsPerMinute;
        }

        #endregion

        #region Private Readonly Fields

        private readonly bool _isHardCoreModeEnabled;

        private readonly int _startingLives;

        private readonly int _maxLives;

        private readonly int? _exerciseTimeLimitInSeconds;

        private readonly double? _minimumKeysPerSecond;

        private readonly double? _minimumWordsPerMinute;

        #endregion

        #region Public Members

        public bool IsHardCoreEnabled => _isHardCoreModeEnabled;

        public int StartingLives => _startingLives; 

        public int MaxLives => _maxLives;

        public int? ExerciseTimeLimitInSeconds => _exerciseTimeLimitInSeconds;

        public double? MinimumKeysPerSecond => _minimumKeysPerSecond;

        public double? MinimumWordsPerMinute => _minimumWordsPerMinute;
        
        #endregion
    }
}

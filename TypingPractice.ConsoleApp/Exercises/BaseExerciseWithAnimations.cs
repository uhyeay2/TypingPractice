using Figgle;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.AsciiArt;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Exercises
{
    public abstract class BaseExerciseWithAnimations : BaseExercise
    {
        protected BaseExerciseWithAnimations(ExerciseSettings settings) : base(settings)
        {
            _accuracyGauge = new(PrimaryFontColor, PrimaryFontColor, BackgroundColor, maxThreshold: 98.5, () => GetAccuracy() ?? 0.00);

            _wordsPerMinuteGauge = new(PrimaryFontColor, PrimaryFontColor, BackgroundColor, WordsPerMinuteGaugeMaxThreshold, () => GetWordsPerMinute() ?? 0.00);

            _timeRemainingProgressBar = new(ConsoleColor.DarkGray, BackgroundColor, HeightOfTimeRemainingProgressBar, settings.ExerciseTimeLimitInSeconds.GetValueOrDefault(), () => settings.ExerciseTimeLimitInSeconds.GetValueOrDefault() - ElapsedExerciseTime.TotalSeconds);
        }

        #region Protected Virtual Methods For Overriding Initialization of Animations

        #endregion

        #region Protected Animation Members

        protected NumberGauge _accuracyGauge;
        protected NumberGauge _wordsPerMinuteGauge;

        protected abstract int WordsPerMinuteGaugeMaxThreshold { get; }

        protected virtual int HeightOfTimeRemainingProgressBar => 33;
        protected VerticalProgressBar _timeRemainingProgressBar;

        public override IEnumerable<Animation> GetAnimations() => [_accuracyGauge, _wordsPerMinuteGauge, _timeRemainingProgressBar];

        #endregion

        #region Protected Methods For Reuseable Displays

        protected DisplayedSection GetRemainingTimeSection(int sectionWidth = 14)
        {
            var progressBar = _timeRemainingProgressBar.Display();

            if (progressBar.Count == 0)
            {
                return [];
            }

            var timeRemaining = new TimeSpan(0, 0, _settings.ExerciseTimeLimitInSeconds.GetValueOrDefault()) - ElapsedExerciseTime;

            return new DisplayedSection(
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "",
                    "Time Left:",
                    "",
                    timeRemaining.TimeStamp(),
                    ""
                ), progressBar
            ).CenteredHorizontal(BackgroundColor, sectionWidth);
        }

        protected DisplayedSection GetAccuracySection()
        {
            var accuracy = GetAccuracy();

            return new(
                _accuracyGauge.Display(),
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "",
                    "Accuracy: " + (accuracy == null ? "N/A" : accuracy.Value.ToString("0.00") + "%"))
            );
        }

        protected DisplayedSection GetWordsPerMinuteSection()
        {
            return new(
                _wordsPerMinuteGauge.Display(),
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "",
                    $"WPM: {GetWordsPerMinute():0.00}")
            );
        }
        
        protected DisplayedSection GetLivesSection()
        {
            if (CurrentLives <= 0)
            {
                return [];
            }

            const int LivesSectionWidth = 27;
            const int LivesSectionHeight = 35;

            var lives = new DisplayedSection(InputBasedColor, BackgroundColor, FiggleFonts.SlantSmall, "Lives:", "");

            for (int i = 0; i < CurrentLives; i++)
            {
                lives.AddRange(new Heart(ConsoleColor.Red, ConsoleColor.DarkRed, BackgroundColor));
            }

            return lives.PadBottom(BackgroundColor, LivesSectionHeight)
                        .CenteredHorizontal(BackgroundColor, LivesSectionWidth);
        }

        #endregion
    }
}

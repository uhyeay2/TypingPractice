using Figgle;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.AsciiArt;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsExercise: BaseExercise
    {
        #region Private Members 

        private readonly string _pool;

        private readonly Random _random = new();

        private NumberGauge _accuracyGauge;

        private NumberGauge _wordsPerMinuteGauge;

        private const int _heightOfTimeRemainingProgressBar = 32;
        private VerticalProgressBar _timeRemainingProgressBar;

        public override IEnumerable<Animation> GetAnimations() => [_accuracyGauge, _wordsPerMinuteGauge, _timeRemainingProgressBar];

        #endregion

        #region Constructors

        public KeyFlashCardsExercise(string pool, ExerciseSettings settings) : base(settings)
        {
            _pool = pool;
            
            _accuracyGauge = new(PrimaryFontColor, PrimaryFontColor, BackgroundColor, maxThreshold: 98.5, () => GetAccuracy() ?? 0.00);

            _wordsPerMinuteGauge = new(PrimaryFontColor, PrimaryFontColor, BackgroundColor, maxThreshold: 30, () => GetWordsPerMinute() ?? 0.00);

            _timeRemainingProgressBar = new(ConsoleColor.DarkGray, BackgroundColor, _heightOfTimeRemainingProgressBar, settings.ExerciseTimeLimitInSeconds.GetValueOrDefault(), () => settings.ExerciseTimeLimitInSeconds.GetValueOrDefault() - ElapsedExerciseTime.TotalSeconds);
        }

        #endregion      

        #region Method Overrides

        private DisplayedSection GetDisplay(bool beforeStarting)
        {
            const int SpeedGaugeWidth = 17;

            var leftSide = new DisplayedSection(GetPromptSection(), GetExpectedInputSection(beforeStarting));
            
            var remainingTimeSection = GetRemainingTimeSection();
            
            if (remainingTimeSection.Count > 0)
            {
                leftSide = remainingTimeSection.AddRightSideSection(BackgroundColor, leftSide);
            }

            var livesSection = GetLivesSection();

            if (livesSection.Count > 0)
            {
                leftSide = livesSection.AddRightSideSection(BackgroundColor, leftSide);
            }

            var rightSide = new DisplayedSection(
                new DisplayedSection (BackgroundColor, BackgroundColor, "", "", ""),
                GetAccuracySection().CenteredHorizontal(BackgroundColor, SpeedGaugeWidth)
                    .AddRightSideSection(BackgroundColor,
                        GetSpeedSection().CenteredHorizontal(BackgroundColor, SpeedGaugeWidth)),
                GetRecentErrorsSection());

            return leftSide
                .AddRightSideSection(BackgroundColor, new(BackgroundColor, BackgroundColor, "   "))
                .AddRightSideSection(BackgroundColor, rightSide).Centered(BackgroundColor);
        }

        private DisplayedSection GetLivesSection()
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

        private DisplayedSection GetRemainingTimeSection()
        {
            var progressBar = _timeRemainingProgressBar.Display();

            if (progressBar.Count == 0)
            {
                return [];
            }           

            const int WidthOfTimeRemainingProgressBar = 14;

            var timeRemaining = new TimeSpan(0, 0, _settings.ExerciseTimeLimitInSeconds.GetValueOrDefault()) - ElapsedExerciseTime;

            return new DisplayedSection(
                new DisplayedSection(PrimaryFontColor, BackgroundColor, 
                    "", 
                    "", 
                    "Time Left:", 
                    "", 
                    timeRemaining.TimeStamp(),
                    "" 
                ), progressBar
            ).CenteredHorizontal(BackgroundColor, WidthOfTimeRemainingProgressBar);
        }

        private DisplayedSection GetExpectedInputSection(bool beforeStarting)
        {
            const int FiggleFontHeight = 21;

            const int ExpectedInputBorderHeight = 27;
            const int ExpectedInputBorderWidth = 38;

            var expectedInput = 
                ( beforeStarting ? new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Press", "Any", "Key")
                                 : new DisplayedSection(InputBasedColor, BackgroundColor, FiggleFonts.Colossal, $"{ExpectedInput}")
                ).CenteredVertical(BackgroundColor, FiggleFontHeight);

            if (!beforeStarting)
            {
                // Add a section that says the expected input and last character typed in standard text above the FiggleFont character
                expectedInput.InsertRange(0, new DisplayedSection(InputBasedColor, BackgroundColor,
                    "",
                    ExpectedInputDescription(),
                    "",
                    $"Last Key Typed: {GetLastCharacterTyped()}"
                ));
            }

            return new BasicBorder(InputBasedColor, BackgroundColor, ExpectedInputBorderWidth, ExpectedInputBorderHeight, expectedInput).ToDisplayedSection();
        }

        private DisplayedSection GetPromptSection()
        {
            const int PromptWidth = 28;
            const int PromptHeight = 11;

            string[] message = ExpectedInput == null || LastKeyTyped == null ? ["Good", "Luck"]
                                            : LastKeyTyped.IsCorrectKeyTyped ? ["Good", "Job"]
                                                                             : ["Try", "Again"];

            return new DisplayedSection(InputBasedColor, BackgroundColor, FiggleFonts.SlantSmall, message)
                .Centered(BackgroundColor, PromptWidth, PromptHeight);
        }

        private DisplayedSection GetAccuracySection()
        {
            var accuracy = GetAccuracy();

            return new(
                _accuracyGauge.Display(),
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "",
                    "Accuracy: " + (accuracy == null ? "N/A" : accuracy.Value.ToString("0.00") + "%"))
            );
        }

        private DisplayedSection GetSpeedSection()
        {
            return new(
                _wordsPerMinuteGauge.Display(),
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "",
                    $"WPM: {GetWordsPerMinute():0.00}")
            );
        }

        private DisplayedSection GetRecentErrorsSection()
        {
            const int Height = 27;
            const int Width = 38;

            return new BasicBorder(ConsoleColor.Red, BackgroundColor, Width, Height,
                new DisplayedSection(
                    new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.SlantSmall, "Recent", "Errors:"),
                    new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.KeyboardSmall,
                        RecentErrors().Split(4).Select(_ => _.Aggregate((a, b) => a += b)).ToArray())
                ).PadBottom(BackgroundColor, Height - 2)
            ).ToDisplayedSection();
        }

        public override DisplayedSection GetDisplayBeforeStarting() =>
            GetDisplay(beforeStarting: true);

        public override DisplayedSection GetDisplayDuringExercise() =>
            GetDisplay(beforeStarting: false);

        public override char? GetExpectedInput()
        {
            // Don't change expected input if last key typed was incorrect
            if (LastKeyTyped != null && !LastKeyTyped.IsCorrectKeyTyped)
            {
                return ExpectedInput;
            }

            var newExpected = ExpectedInput;

            // loop until we get a new input that is not the same as previous expectedInput
            do
            {
                var lastErrors = RecentErrors();

                var poolWithErrors = lastErrors.Any() ? _pool + lastErrors.Aggregate((a, b) => a += b) : _pool;

                newExpected = poolWithErrors.ElementAt(_random.Next(0, poolWithErrors.Length));

            } while (newExpected == LastKeyTyped?.CharacterTyped);

            return newExpected;
        }

        //TODO: Implement GameOver Screen  
        public override Screen GetGameOverScreen() => new OpeningScreen();

        public override PauseScreen GetPauseScreen() => new PauseScreen(this, new KeyFlashCardsMainMenu(), GenerateScoresContent());
        
        #endregion

        #region Helpers

        private IEnumerable<string> RecentErrors() =>
            KeysTyped.TakeLast(50)
                     .Where(_ => _.IsCorrectKeyTyped && _.PreviousKeyTyped != null && !_.PreviousKeyTyped.IsCorrectKeyTyped && _pool.Contains(_.CharacterTyped))
                     .DistinctBy(_ => _.CharacterTyped)
                     .Select(_ => _.CharacterTyped.ToString());

        private DisplayedSection GenerateScoresContent()
        {
            var countOfKeysTyped = KeysTyped.Count;

            if (countOfKeysTyped == 0)
            {
                return new(SecondaryFontColor, BackgroundColor,
                    "",
                    $"Timer: {ElapsedExerciseTime.TimeStamp()}",
                    "",
                    "Avg Speed: N/A",
                    "",
                    $"Keys Per Second: N/A",
                    "",
                    $"Words Per Minute: N/A",
                    "",
                    $"Accuracy: N/A"
                );
            }

            var countOfCorrectKeysTyped = KeysTyped.Count(_ => _.CharacterTyped == _.ExpectedCharacter);

            return new(SecondaryFontColor, BackgroundColor,
                "",
                $"Timer: {ElapsedExerciseTime.TimeStamp()}",
                "",
                $"Avg Speed: {KeysTyped.Average(_ => _.Milliseconds) / 1000:0.00}s",
                "",
                $"Keys Per Minute: {GetKeysPerMinute():0.00}",
                "",
                $"Words Per Minute: {GetWordsPerMinute():0.00}",
                "",
                $"Accuracy: {(double)countOfCorrectKeysTyped / countOfKeysTyped:0.00%} ({countOfCorrectKeysTyped}/{countOfKeysTyped})"
            );
        }

        public string ExpectedInputDescription()
        {
            if (ExpectedInput == null)
            {
                return string.Empty;
            }

            if (char.IsWhiteSpace(ExpectedInput.Value))
            {
                return "    Space   ";
            }

            if (char.IsUpper(ExpectedInput.Value))
            {
                return $"UPPERCASE: {ExpectedInput}";
            }

            if (char.IsLower(ExpectedInput.Value))
            {
                return $"lowercase: {ExpectedInput}";
            }

            if (char.IsNumber(ExpectedInput.Value))
            {
                return $"   Number: {ExpectedInput}";
            }

            return $"  Special: {ExpectedInput}";
        }

        private char GetLastCharacterTyped() => LastKeyTyped?.CharacterTyped switch
        {
            null or '\n' or '\r' or '\t' or '\b' or '\0' => ' ',
            _ => LastKeyTyped.CharacterTyped,
        };

        #endregion
    }
}

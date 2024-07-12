using Figgle;
using TypingPractice.ConsoleApp.Display.AsciiArt;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.ExpectedInputGenerators;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsExercise: BaseExerciseWithAnimations
    {
        #region Private Members 

        private readonly ExpectedInputGenerator<char> _expectedInputGenerator;

        #endregion

        protected override int WordsPerMinuteGaugeMaxThreshold => 30;

        #region Constructors

        public KeyFlashCardsExercise(ExpectedInputGenerator<char> expectedInputGenerator, ExerciseSettings settings) : base(settings)
        {
            _expectedInputGenerator = expectedInputGenerator;            
        }

        #endregion      

        #region Method Overrides

        private DisplayedSection GetDisplay(bool beforeStarting)
        {
            const int SpeedGaugeWidth = 17;
            
            var expectedInputSection = new DisplayedSection(GetPromptSection(), GetExpectedInputSection(beforeStarting));

            var statsSection = new DisplayedSection(
                new DisplayedSection (BackgroundColor, BackgroundColor, "", "", ""),
                GetAccuracySection().CenteredHorizontal(BackgroundColor, SpeedGaugeWidth)
                    .AddRightSideSection(BackgroundColor,
                        GetWordsPerMinuteSection().CenteredHorizontal(BackgroundColor, SpeedGaugeWidth)),
                GetRecentErrorsSection());
            
            return expectedInputSection
                .AddRightSideSection(BackgroundColor, new(BackgroundColor, BackgroundColor, "   "))
                .AddRightSideSection(BackgroundColor, statsSection)
                .AddRightSideSection(BackgroundColor, GetRemainingTimeSection())
                .AddRightSideSection(BackgroundColor, GetLivesSection())
                .Centered(BackgroundColor);
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

            return new DisplayInsideSimpleBorder(InputBasedColor, BackgroundColor, ExpectedInputBorderWidth, ExpectedInputBorderHeight, expectedInput);
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

        private DisplayedSection GetRecentErrorsSection()
        {
            const int Height = 27;
            const int Width = 38;

            return new DisplayInsideSimpleBorder(ConsoleColor.Red, BackgroundColor, Width, Height,
                new DisplayedSection(
                    new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.SlantSmall, "Recent", "Errors:"),
                    new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.KeyboardSmall,
                        RecentErrors().Split(4).Select(_ => _.Aggregate((a, b) => a += b)).ToArray())
                ).PadBottom(BackgroundColor, Height - 2)
            );
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

            return _expectedInputGenerator.GetNextExpectedInput();
        }

        //TODO: Implement GameOver Screen  
        public override Screen GetGameOverScreen() => new OpeningScreen();

        public override PauseScreen GetPauseScreen() => new PauseScreen(this, new MainMenu(), GenerateScoresContent());

        #endregion

        #region Helpers

        private IEnumerable<string> RecentErrors() =>
            KeysTyped.TakeLast(50)
                     .Where(_ => _.IsCorrectKeyTyped && _.PreviousKeyTyped != null && !_.PreviousKeyTyped.IsCorrectKeyTyped)
                     .DistinctBy(_ => _.ExpectedCharacter)
                     .Take(16)
                     .Select(_ => _.ExpectedCharacter.ToString());

        //TODO: Replace/Update this - it's only used for pause, pause needs to be reworked and a game over screen created
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

        public override void NotifyExpectedInputGeneratorOfMistake()
        {
            if (LastKeyTyped != null && !LastKeyTyped.IsCorrectKeyTyped)
            {
                _expectedInputGenerator.NotifyOfMistake(LastKeyTyped.ExpectedCharacter);
            }            
        }

        #endregion
    }
}

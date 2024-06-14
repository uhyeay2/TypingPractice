using Figgle;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsExerciseScreen : ExerciseScreen
    {
        private const int OutterBorderWidth = 78;

        private const int OutterBorderHeight = 29;

        private const int ContentSectionWidth = 37;

        private const int InnerBorderWidth = 35;

        private const int TopLeftBorderHeight = 6;

        private const int BottomLeftBorderHeight = 18;

        private const int ScoresBorderHeight = 5;

        private const int ResponsiveMessageHeight = 11;

        private readonly string _pool;

        private readonly Random _random = new();

        public KeyFlashCardsExerciseScreen(string pool) => _pool = pool;

        private DisplayedSection GetLeftSideScreenContent(char? c, KeyTypedStat? lastKeyTyped) => c == null ?
            // Left Side Content Before Typing Started (expected Char not yet generated)
            new DisplayedSection(
                new BasicBorder(SecondaryFontColor, BackgroundColor, InnerBorderWidth, TopLeftBorderHeight,
                    new DisplayedSection(PrimaryFontColor, BackgroundColor,
                        "", 
                        "Press Any Key", 
                        "", 
                        "To Start Typing")
                ).ToDisplayedSection(),
                new DisplayedSection(BackgroundColor, BackgroundColor, ""),
                new BasicBorder(SecondaryFontColor, BackgroundColor, InnerBorderWidth, BottomLeftBorderHeight,
                    new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, 
                        "Press", 
                        "Any", 
                        "Key")
                ).ToDisplayedSection()
            )
            : // Left Side Content when Typing has started (expected Char has been generated)
            new DisplayedSection(
                new BasicBorder(InputBasedColor(c, lastKeyTyped), BackgroundColor, InnerBorderWidth, TopLeftBorderHeight,
                    new DisplayedSection(InputBasedColor(c, lastKeyTyped), BackgroundColor, 
                        "", 
                        c.Value.ExpectedCharacterDescription(), 
                        "", 
                        $"Last Key Typed: {GetLastCharacterTyped(lastKeyTyped)}")
                ).ToDisplayedSection(),
                new DisplayedSection(BackgroundColor, BackgroundColor, ""),
                new BasicBorder(InputBasedColor(c, lastKeyTyped), BackgroundColor, InnerBorderWidth, BottomLeftBorderHeight,
                    new DisplayedSection(InputBasedColor(c, lastKeyTyped), BackgroundColor, FiggleFonts.Colossal, 
                        c.Value.ToString())
                ).ToDisplayedSection()
            );

        private DisplayedSection GetRightSideScreenContent(char? c, KeyTypedStat? lastKeyTyped) => new(
            new DisplayedSection(SecondaryFontColor, BackgroundColor, "", $"Timer: {ElapsedExerciseTime}"), 
            new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Score", ""),
            new BasicBorder(SecondaryFontColor, BackgroundColor, InnerBorderWidth, ScoresBorderHeight,GenerateScoresContent()).ToDisplayedSection(),
            new DisplayedSection(InputBasedColor(c, lastKeyTyped), BackgroundColor, FiggleFonts.SlantSmall, GetPromptMessage(c, lastKeyTyped)).CenteredVertical(BackgroundColor, ResponsiveMessageHeight)
        );

        private ConsoleColor InputBasedColor(char? c, KeyTypedStat? lastKeyTyped) =>
           c == null || lastKeyTyped == null ? SecondaryFontColor : lastKeyTyped.IsCorrectKeyTyped ? ConsoleColor.Green : ConsoleColor.Red;

        private string[] GetPromptMessage(char? c, KeyTypedStat? lastKeyTyped) =>
            c == null ? ["Start", "Typing"]
            : lastKeyTyped == null ? ["Good", "Luck"]
            : lastKeyTyped.IsCorrectKeyTyped ? ["Good", "Job"]
            : ["", "Try", "Again"];

        private DisplayedSection GenerateScreenContent(char? c, KeyTypedStat? lastKeyTyped) => new(
            new BasicBorder(SecondaryFontColor, BackgroundColor, OutterBorderWidth, OutterBorderHeight, 
                GetLeftSideScreenContent(c, lastKeyTyped).CenteredHorizontal(BackgroundColor, ContentSectionWidth)
                .AddRightSideSection(BackgroundColor, GetRightSideScreenContent(c, lastKeyTyped).CenteredHorizontal(BackgroundColor, ContentSectionWidth))
            ).ToDisplayedSection()
            .AddRightSideSection(BackgroundColor, RecentErrorsSection())
            .Centered(BackgroundColor)
        );

        private DisplayedSection RecentErrorsSection() => 
            new BasicBorder(ConsoleColor.Red, BackgroundColor, ContentSectionWidth, OutterBorderHeight,
                new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.SlantSmall, "Recent", "Errors:"),
                new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.KeyboardSmall, 
                    RecentErrors().Split(4).Select(_ => _.Aggregate((a, b) => a += b)).ToArray())
            ).ToDisplayedSection();

        private char GetLastCharacterTyped(KeyTypedStat? lastKeyTyped) => lastKeyTyped?.CharacterTyped switch
        {
            null or '\n' or '\r' or '\t' or '\b' or '\0' => ' ',
            _ => lastKeyTyped.CharacterTyped,
        };

        private DisplayedSection GenerateScoresContent()
        {
            var countOfKeysTyped = KeysTyped.Count;

            if (countOfKeysTyped == 0)
            {     
                return new(SecondaryFontColor, BackgroundColor,                     
                    "", 
                    "Avg Speed: N/A",
                    "",
                    $"Accuracy: N/A"
                );
            }

            var countOfCorrectKeysTyped = KeysTyped.Count(_ => _.CharacterTyped == _.ExpectedCharacter);

            return new(SecondaryFontColor, BackgroundColor,                
                "", 
                $"Avg Speed: {KeysTyped.Average(_ => _.Milliseconds) / 1000:0.00}s",
                "",
                $"Accuracy: {(double)countOfCorrectKeysTyped / countOfKeysTyped:0.00%} ({countOfCorrectKeysTyped}/{countOfKeysTyped})" 
            );
        }

        public override Screen GetNextScreen() => new PauseScreen(this, new KeyFlashCardsMainMenu(), GenerateScoresContent());

        public override void StartExerciseLoop()
        {
            if (LastKeyTyped?.IsCorrectKeyTyped ?? true)
            {
                do
                {
                    var lastErrors = RecentErrors();

                    var poolWithErrors = lastErrors.Any() ? _pool + lastErrors.Aggregate((a, b) => a += b) : _pool;

                    ExpectedCharacter = poolWithErrors.ElementAt(_random.Next(0, poolWithErrors.Length));

                } while (ExpectedCharacter == LastKeyTyped?.CharacterTyped);
            }
        }

        private IEnumerable<string> RecentErrors() => 
            KeysTyped.TakeLast(50)
                     .Where(_ => _.IsCorrectKeyTyped && _.PreviousKeyTyped != null && !_.PreviousKeyTyped.IsCorrectKeyTyped && _pool.Contains(_.CharacterTyped))
                     .DistinctBy(_ => _.CharacterTyped)
                     .Select(_ => _.CharacterTyped.ToString());

        public override DisplayedSection GetDisplay() => GenerateScreenContent(ExpectedCharacter, LastKeyTyped);
    }
}

using Figgle;
using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsExerciseScreen : Screen
    {
        private const int OutterBorderWidth = 89;

        private const int OutterBorderHeight = 27;

        private const int InnerContentWidth = 42;

        private const int InnerBorderWidth = 40;

        private const int TopLeftBorderHeight = 6;

        private const int BottomLeftBorderHeight = 16;

        private const int ScoresBorderHeight = 8;

        private const int ResponsiveMessageHeight = 10;

        private readonly string _pool;

        private readonly Random _random = new();

        private readonly List<KeyTypedStat> _keysTyped = new();       

        public KeyFlashCardsExerciseScreen(string pool) => _pool = pool;

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            var keepTyping = true;

            var stopWatch = new Stopwatch();

            char? expectedCharacter = null;

            KeyTypedStat? lastKeyTyped = null;

            Print(GenerateScreenContent(expectedCharacter, lastKeyTyped), clearScreen: true);

            Console.ReadKey(true);

            while (keepTyping)
            {
                Console.SetCursorPosition(0, 0);

                // generate new character when last key typed was correct or null
                if (lastKeyTyped?.IsCorrectKeyTyped ?? true)
                {
                    expectedCharacter = _pool[_random.Next(0, _pool.Length)];
                }

                Print(GenerateScreenContent(expectedCharacter, lastKeyTyped));

                stopWatch.Restart();

                var input = Console.ReadKey(true);

                var milliseconds = stopWatch.Elapsed.TotalMilliseconds;

                if (input.Key == ConsoleKey.Escape || input.Key == ConsoleKey.Enter)
                {
                    keepTyping = false;
                }
                else
                {
                    lastKeyTyped = new(lastKeyTyped, milliseconds, input.KeyChar, expectedCharacter.GetValueOrDefault());

                    _keysTyped.Add(lastKeyTyped);
                }
            }

            return new PauseScreen(this, new KeyFlashCardsMainMenu(), GenerateScoresContent());
        }

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
            new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, 
                "Score"),
            new BasicBorder(SecondaryFontColor, BackgroundColor, InnerBorderWidth, ScoresBorderHeight, 
                GenerateScoresContent()
            ).ToDisplayedSection(),
            new DisplayedSection(InputBasedColor(c, lastKeyTyped), BackgroundColor, FiggleFonts.SlantSmall, 
                GetPromptMessage(c, lastKeyTyped)
            ).CenteredVertical(BackgroundColor, ResponsiveMessageHeight)
        );

        private ConsoleColor InputBasedColor(char? c, KeyTypedStat? lastKeyTyped) =>
           c == null || lastKeyTyped == null ? SecondaryFontColor
           : lastKeyTyped.IsCorrectKeyTyped ? ConsoleColor.Green : ConsoleColor.Red;

        private string[] GetPromptMessage(char? c, KeyTypedStat? lastKeyTyped) =>
            c == null ? ["", "Start", "Typing"]
            : lastKeyTyped == null ? ["Good", "Luck"]
            : lastKeyTyped.IsCorrectKeyTyped ? ["Good", "Job"]
            : ["", "Try", "Again"];

        private DisplayedSection GenerateScreenContent(char? c, KeyTypedStat? lastKeyTyped) => new(
            new BasicBorder(SecondaryFontColor, BackgroundColor, OutterBorderWidth, OutterBorderHeight, 
                GetLeftSideScreenContent(c, lastKeyTyped).CenteredHorizontal(BackgroundColor, InnerContentWidth)
                .AddRightSideSection(BackgroundColor, GetRightSideScreenContent(c, lastKeyTyped).CenteredHorizontal(BackgroundColor, InnerContentWidth))
            ).ToDisplayedSection().Centered(BackgroundColor)
        );

        private char GetLastCharacterTyped(KeyTypedStat? lastKeyTyped) =>
            (lastKeyTyped?.CharacterTyped) switch
            {
                null or '\n' or '\r' or '\t' or '\b' or '\0' => ' ',
                _ => lastKeyTyped.CharacterTyped,
            };

        private DisplayedSection GenerateScoresContent()
        {
            var countOfKeysTyped = _keysTyped.Count;

            if (countOfKeysTyped == 0)
            {
                return new(SecondaryFontColor, BackgroundColor, 
                    "",
                    $"Amount Correct: N/A",
                    "",
                    $"Accuracy: N/A",
                    "",
                    $"Average Speed: N/A"
                );
            }

            var countOfCorrectKeysTyped = _keysTyped.Count(_ => _.CharacterTyped == _.ExpectedCharacter);

            return new(SecondaryFontColor, BackgroundColor,
                "",
                $"Amount Correct: {countOfCorrectKeysTyped} / {countOfKeysTyped}",
                "",
                $"Accuracy: "+ ((double)countOfCorrectKeysTyped / countOfKeysTyped).ToString("0.00%"),
                "",
                $"Average Speed: {_keysTyped.Average(_ => _.Milliseconds) / 1000:0.00} seconds"
            );
        }
    }
}

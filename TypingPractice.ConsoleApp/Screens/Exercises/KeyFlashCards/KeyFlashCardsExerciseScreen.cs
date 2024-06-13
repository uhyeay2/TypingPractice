using Figgle;
using System.Diagnostics;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.SplashScreens;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsExerciseScreen : Screen
    {
        private const int InnerBorderWidth = 40;

        private const int LeftContentFiggleFontHeight = 15;

        private const int ResponsiveMessageFiggleFontHeight = 10;

        private const int ScoresHeaderFiggleFontHeight = 7;

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

            _consoleWriter.WriteLines(GenerateScreenContent(expectedCharacter, lastKeyTyped));

            Console.ReadKey(true);

            while (keepTyping)
            {
                Console.SetCursorPosition(0, 0);

                // generate new character when last key typed was correct or null
                if (lastKeyTyped?.IsCorrectKeyTyped ?? true)
                {
                    expectedCharacter = _pool[_random.Next(0, _pool.Length)];
                }

                _consoleWriter.WriteLines(GenerateScreenContent(expectedCharacter, lastKeyTyped));

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

            return new PauseScreen(this, new KeyFlashCardsMainMenu(), Border.Apply(GenerateScoresContent()));
        }

        private IEnumerable<string> GetLeftSideScreenContent(char? c, KeyTypedStat? lastKeyTyped) => c == null ?
            // Left Side Content Before Typing Started (expected Char not yet generated)
            Border.Apply(InnerBorderWidth,
                "Press Any Key", "", "To Start Typing")
            .Append("").Concat(
            Border.Apply(InnerBorderWidth,
                FiggleFonts.SlantSmall.RenderIEnumerable(LeftContentFiggleFontHeight,
                    "Press", "Any", "Key")
            ))
            : // Left Side Content when Typing has started (expected Char has been generated)
            Border.Apply(InnerBorderWidth,
                c.Value.ExpectedCharacterDescription(), "", $"Last Key Typed: {GetLastCharacterTyped(lastKeyTyped)}")
            .Append("").Concat(
            Border.Apply(InnerBorderWidth,
                FiggleFonts.Colossal.RenderIEnumerable(LeftContentFiggleFontHeight,
                    c.Value.ToString())
            ));

        private IEnumerable<string> GetRightSideScreenContent(char? c, KeyTypedStat? lastKeyTyped) =>
            FiggleFonts.SlantSmall.RenderIEnumerable(ScoresHeaderFiggleFontHeight, "Score")
            .Concat(Border.Apply(InnerBorderWidth, GenerateScoresContent()))
            .Concat(FiggleFonts.SlantSmall.RenderIEnumerable(ResponsiveMessageFiggleFontHeight,
                c == null ? ["", "Start", "Typing"]
                : lastKeyTyped == null ? ["Good", "Luck"]
                : lastKeyTyped.IsCorrectKeyTyped ? ["Good", "Job"]
                : ["Try", "Again"])).Append("");

        private ScreenContent GenerateScreenContent(char? c, KeyTypedStat? lastKeyTyped) => _contentBuilder.New(_ =>
            _.AddContent(Center.HorizontalByConsoleWidth(Border.Apply(
                GetLeftSideScreenContent(c, lastKeyTyped)
                .MergeToRight(GetRightSideScreenContent(c, lastKeyTyped), middlePadding: "  ")
            ))));

        private char GetLastCharacterTyped(KeyTypedStat? lastKeyTyped) =>
            (lastKeyTyped?.CharacterTyped) switch
            {
                null or '\n' or '\r' or '\t' or '\b' or '\0' => ' ',
                _ => lastKeyTyped.CharacterTyped,
            };

        private IEnumerable<string> GenerateScoresContent()
        {
            var countOfKeysTyped = _keysTyped.Count;

            if (countOfKeysTyped == 0)
            {
                return [
                    $"Amount Correct: N/A",
                    "",
                    $"Accuracy: N/A",
                    "",
                    $"Average Speed: N/A"
                ];
            }

            var countOfCorrectKeysTyped = _keysTyped.Count(_ => _.CharacterTyped == _.ExpectedCharacter);

            return [
                $"Amount Correct: {countOfCorrectKeysTyped} / {countOfKeysTyped}",
                "",
                $"Accuracy: "+ ((double)countOfCorrectKeysTyped / countOfKeysTyped).ToString("0.00%"),
                "",
                $"Average Speed: {_keysTyped.Average(_ => _.Milliseconds) / 1000:0.00} seconds"
            ];
        }
    }
}

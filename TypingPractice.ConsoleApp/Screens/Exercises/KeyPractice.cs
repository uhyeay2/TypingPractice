using Figgle;
using System.Diagnostics;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.SplashScreens;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.Exercises
{
    internal class KeyPractice : Screen
    {
        private const int InnerBorderWidth = 40;

        private const int LeftContentFiggleFontHeight = 15;

        private const int ResponsiveMessageFiggleFontHeight = 10;

        private const int ScoresHeaderFiggleFontHeight = 7;

        private readonly string _pool;

        private readonly Random _random = new();

        private readonly List<KeyTypedStat> _keysTyped = new();

        public KeyPractice() => _pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWZYZ1234567890-=!@#$%^&*()_+[]{};:'\"/?";

        public KeyPractice(string pool) => _pool = pool;       

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

                if (input.Key == ConsoleKey.Escape)
                {
                    keepTyping = false;
                }
                else
                {
                    lastKeyTyped = new(lastKeyTyped, milliseconds, input.KeyChar, expectedCharacter.GetValueOrDefault());

                    _keysTyped.Add(lastKeyTyped);
                }
            }

            _consoleWriter.ClearThenWriteLines(_contentBuilder
                .New( _ =>
                    _.AddContent(
                        FiggleFonts.SlantSmall.RenderIEnumerable("Score", ""),
                        Border.Apply(GetScores()),
                        ["", "Press Any Key To Continue"]
                    )
                    .ApplyBorder()
                    .CenterByConsoleSize()
                ));

            Console.ReadKey(true);

            return new ClosingScreen();
        }

        private ScreenContent GenerateScreenContent(char? c, KeyTypedStat? lastKeyTyped) => _contentBuilder.New(_ =>
            _.AddContent(c == null ? 
                [   // Left Side Content Before Starting (expected Char not yet generated)
                    Border.Apply(
                        [
                            "Press Any Key", 
                            "",
                            "To Start Typing"
                        ], InnerBorderWidth)
                    .Append(""),
                    Border.Apply(
                        FiggleFonts.SlantSmall.RenderIEnumerable(LeftContentFiggleFontHeight, 
                            "Press", 
                            "Any", 
                            "Key"), InnerBorderWidth)
                ]:[
                    // Left Side Content when Typing has started (expected Char has been generated)
                    Border.Apply(
                        [
                            c.Value.ExpectedCharacterDescription(), 
                            "",
                            $"Last Key Typed: {_keysTyped.LastOrDefault()?.CharacterTyped ?? ' '}"
                        ], InnerBorderWidth)
                    .Append(""),
                    Border.Apply(
                        FiggleFonts.Univers.RenderIEnumerable(
                            c.Value.ToString(), LeftContentFiggleFontHeight)
                        , InnerBorderWidth)
                ])
            .MergeRightSideContent(_ => 
                _.AddContent( // Right Side Content
                    FiggleFonts.SlantSmall.RenderIEnumerable(ScoresHeaderFiggleFontHeight, "Score"),
                    Border.Apply(
                        GetScores()
                        , InnerBorderWidth),
                    FiggleFonts.SlantSmall.RenderIEnumerable(ResponsiveMessageFiggleFontHeight,
                        c == null ? ["", "Start", "Typing"]
                        : lastKeyTyped == null ? ["Good", "Luck"]
                        : lastKeyTyped.IsCorrectKeyTyped ? ["Good", "Job"]
                        : ["Try", "Again"])
                ), middlePadding: "  ")
            .AddContent("")
            .ApplyBorder()
            .CenterByConsoleSize(vertical: false));

        private IEnumerable<string> GetScores()
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

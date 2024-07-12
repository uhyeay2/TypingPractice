using Figgle;
using System.Diagnostics;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.ExpectedInputGenerators;
using TypingPractice.ConsoleApp.Models;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.ScrollingWords
{
    public class ScrollingWordsExercise : BaseExerciseWithAnimations
    {
        private readonly ExpectedInputGenerator<string> _expectedInputGenerator;

        private readonly List<WordTypedStat> _wordsTyped = [];

        private readonly List<WordTypedStat> _nextWords = [];

        private WordTypedStat _currentWord;

        private const int _countOfNextWordsToShow = 10;

        private readonly Stopwatch _currentWordTimer = new();

        protected override int WordsPerMinuteGaugeMaxThreshold => 100;
        protected override int HeightOfTimeRemainingProgressBar => 23;


        public ScrollingWordsExercise(ExpectedInputGenerator<string> expectedInputGenerator, ExerciseSettings settings) : base(settings)
        {
            _expectedInputGenerator = expectedInputGenerator;

            _currentWord = new WordTypedStat(expectedInputGenerator.GetNextExpectedInput());

            UpdateNextWordsList();
        }

        private void UpdateCurrentWord()
        {   
            _currentWord = _nextWords[0];

            _nextWords.RemoveAt(0);

            UpdateNextWordsList();
        }

        private void UpdateNextWordsList()
        {
            while (_nextWords.Count < _countOfNextWordsToShow)
            {
                _nextWords.Add(new(_expectedInputGenerator.GetNextExpectedInput()));
            }
        }

        public override DisplayedSection GetDisplayBeforeStarting()
        {
            return GetDisplay(beforeStarting: true);
        }

        public override DisplayedSection GetDisplayDuringExercise()
        {
            return GetDisplay(beforeStarting: false);
        }

        private DisplayedSection GetDisplay(bool beforeStarting) =>
            new DisplayedSection(
                    ExpectedInputSection(beforeStarting),
                    GetAccuracySection().AddRightSideSection(BackgroundColor, GetWordsPerMinuteSection())
                )
                .AddRightSideSection(BackgroundColor, GetRemainingTimeSection())
                .AddRightSideSection(BackgroundColor, GetLivesSection())
                .Centered(BackgroundColor);

        private DisplayedSection ExpectedInputSection(bool beforeStarting)
        {
            const int Width = 39;
            const int Height = 10;

            const int WidthOfTextInsideBorder = 33;

            const int WidthInsideBorder = 35;
            const int HeightInsideBorder = 8;

            const ConsoleColor BorderColor = ConsoleColor.DarkGray;

            if (beforeStarting)
            {
                return new DisplayedSection(InputBasedColor, BackgroundColor, "Press any key to begin")
                    .Centered(BackgroundColor, WidthInsideBorder, HeightInsideBorder)
                    .Centered(BorderColor, Width, Height);
            }

            var leftSideLength = (int) double.Floor(WidthOfTextInsideBorder / 2);

            var content = new DisplayedLine(
                KeysTyped.Where(_ => _.IsCorrectKeyTyped)
                         .TakeLast(leftSideLength)
                         .Select(_ => new DisplayedString(_.CharacterTyped, (_.PreviousKeyTyped?.IsCorrectKeyTyped ?? true) ? ConsoleColor.Green : ConsoleColor.Red, BackgroundColor))
                         .ToList()
                );

            if (content.Width < leftSideLength)
            {
                content.Insert(0, new DisplayedString(' ', leftSideLength - content.Width, BackgroundColor, BackgroundColor));
            }

            if (ExpectedInput == ' ')
            {
                content.Add(new DisplayedString(' ', ConsoleColor.Gray, BackgroundColor));
            }

            content.Add(new DisplayedString(_currentWord.ExpectedWord[_currentWord.CountOfCorrectlyTypedKeys..], ConsoleColor.Gray, BackgroundColor));

            var indexOfNextWordToAdd = 0;

            while (content.Width < WidthOfTextInsideBorder)
            {
                if (indexOfNextWordToAdd < _nextWords.Count)
                {
                    content.Add(new DisplayedString(' ', ConsoleColor.Gray, BackgroundColor));

                    if (content.Width < WidthOfTextInsideBorder)
                    {
                        var word = _nextWords[indexOfNextWordToAdd];
                    
                        if (content.Width + word.ExpectedWord.Length <= WidthOfTextInsideBorder)
                        {
                            content.Add(new DisplayedString(word.ExpectedWord, ConsoleColor.Gray, BackgroundColor));
                        }
                        else
                        {
                            content.Add(new DisplayedString(word.ExpectedWord[..(WidthOfTextInsideBorder - content.Width)], ConsoleColor.Gray, BackgroundColor));
                        }
                    }

                    indexOfNextWordToAdd++;
                }
                else
                {
                    content.Add(new DisplayedString(' ', WidthOfTextInsideBorder - content.Width, ConsoleColor.Gray, BackgroundColor));
                }
            }

            var colorOfArrow = _currentWord.KeysTyped.Any(_ => !_.IsCorrectKeyTyped) ? ConsoleColor.Red : ConsoleColor.Green;

            return new DisplayedSection(
                new DisplayedSection(content),
                new DisplayedSection(colorOfArrow, BackgroundColor, 
                    "  .  ",
                   @" /|\ ",
                    "  |  "
                    )
                ).Centered(BackgroundColor, WidthInsideBorder, HeightInsideBorder)
                .Centered(BorderColor, Width, Height);
        }

        public override char? GetExpectedInput()
        {
            if (LastKeyTyped != null && LastKeyTyped.CharacterTyped != ' ')
            {
                _currentWord.AddKeyTyped(LastKeyTyped);
            }

            if (!_currentWordTimer.IsRunning)
            {
                _currentWordTimer.Start();
            }

            // Don't change expected input if last key typed was incorrect or no key has been typed yet
            if (LastKeyTyped != null && !LastKeyTyped.IsCorrectKeyTyped)
            {
                return ExpectedInput;
            }

            if (_currentWord.IsWordCompleted)
            {
                _wordsTyped.Add(_currentWord);

                UpdateCurrentWord();

                _currentWordTimer.Restart();

                return ' ';
            }

            return _currentWord.ExpectedWord[_currentWord.CountOfCorrectlyTypedKeys];
        }

        public override Screen GetGameOverScreen() => new MainMenu();

        public override PauseScreen GetPauseScreen() => new PauseScreen(this, new MainMenu());

        public override void NotifyExpectedInputGeneratorOfMistake()
        {            
            if (LastKeyTyped != null && !LastKeyTyped.IsCorrectKeyTyped && _currentWord.KeysTyped.Count(_ => !_.IsCorrectKeyTyped) == 1)
            {
                _expectedInputGenerator.NotifyOfMistake(_currentWord.ExpectedWord);
            }
        }            
    }
}

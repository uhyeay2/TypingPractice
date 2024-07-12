using Figgle;
using TypingPractice.ConsoleApp.Constants;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.Settings
{
    public class GetExerciseSettingsScreen : RefreshableScreen
    {
        private readonly Func<ExerciseSettings, BaseExercise> _funcGetExerciseWithSelectedSettings;

        private int _currentIndex = 0;

        private const int _startExerciseSectionIndex = 0;
        private const int _liveSectionIndex = 1;
        private const int _minWordsPerMinuteSectionIndex = 2;
        private const int _timeLimitSectionIndex = 3;
        private const int _maxIndex = 3;

        private int _startingLives = 0;
        private int? _minWordsPerMinute = null;
        private int? _exerciseTimerInSeconds = null;
        
        private bool _selectionMade;

        private const int TopSectionHeight = 10;
        private const int SectionHeight = 8;
        private const int SectionWidth = 100;

        private DisplayedSection SectionDivider => new(
            new DisplayedLine('=', SectionWidth, PrimaryBorderColor, BackgroundColor)
        );

        public GetExerciseSettingsScreen(Func<ExerciseSettings, BaseExercise> funcGetExerciseWithSelectedSettings)
        {
            _funcGetExerciseWithSelectedSettings = funcGetExerciseWithSelectedSettings;            
        }
        
        private DisplayedSection StartSection(bool isSelectedSection)
        {
            if (isSelectedSection)
            {
                return new DisplayedSection(
                    new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, FiggleFonts.SlantSmall,
                        "Start Game"),
                    new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor,
                        "",
                        "Press Enter To Start The Game",
                        "",
                        "Press Up/Down To Edit Any Setting")
                );
            }
            else
            {
                return new DisplayedSection(
                    new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, FiggleFonts.SlantSmall,
                        "Editing Settings"),
                    new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor,
                        "",
                        "Press Left/Right To Edit The Selected Setting",
                        "",
                        "Press Up/Down To Return To Top When Ready To Start")
                );
            }
        }

        private DisplayedSection LivesSection(bool isSelectedSection)
        {
            var livesToDisplay = _startingLives == 0 ? "Unlimited Lives"
                               : _startingLives == ExerciseSettingConstant.MaxLives ? $"{ExerciseSettingConstant.MaxLives} Lives (Max)"
                               : $"{_startingLives} Lives";

            return new DisplayedSection(
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, FiggleFonts.SlantSmall, "Starting Lives"),
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, "", livesToDisplay)
            );
        }

        private DisplayedSection MinimumWordsPerMinuteSection(bool isSelectedSection)
        {
            var minWordsPerMinuteToDisplay = _minWordsPerMinute == null ? "No Minimum Speed"
                                           : _minWordsPerMinute == ExerciseSettingConstant.MaxWordsPerMinuteRequired ? $"Minimum Speed: {ExerciseSettingConstant.MaxWordsPerMinuteRequired} WPM (Max)"
                                           : $"Minimum Speed: {_minWordsPerMinute} WPM";

            return new DisplayedSection(
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, FiggleFonts.SlantSmall, "Words Per Minute"),
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, "", minWordsPerMinuteToDisplay)
            );
        }

        private DisplayedSection ExerciseTimeLimitSection(bool isSelectedSection)
        {
            var currentTimerSet = new TimeSpan(0, 0, _exerciseTimerInSeconds.GetValueOrDefault());

            var currentTimerString = $"{currentTimerSet:hh}h {currentTimerSet:mm}m {currentTimerSet:ss}s";

            var timeLimitToDisplay = _exerciseTimerInSeconds == null ? "No Time Limit"
                                   : _exerciseTimerInSeconds == ExerciseSettingConstant.MaxExerciseTimerInSeconds ? $"Type For: {currentTimerString} (Max)"
                                   : $"Type For: {currentTimerString}";

            return new DisplayedSection(
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, FiggleFonts.SlantSmall, "Timed Exercise"),
                new DisplayedSection(GetSectionFontColor(isSelectedSection), BackgroundColor, "", timeLimitToDisplay)
            );
        }

        public override DisplayedSection GetDisplay()
        {
            return new DisplayedSection(
                StartSection(_currentIndex == _startExerciseSectionIndex).Centered(BackgroundColor, SectionWidth, TopSectionHeight),
                SectionDivider,
                LivesSection(_currentIndex == _liveSectionIndex).Centered(BackgroundColor, SectionWidth, SectionHeight),
                SectionDivider,
                MinimumWordsPerMinuteSection(_currentIndex == _minWordsPerMinuteSectionIndex).Centered(BackgroundColor, SectionWidth, SectionHeight),
                SectionDivider,
                ExerciseTimeLimitSection(_currentIndex == _timeLimitSectionIndex).Centered(BackgroundColor, SectionWidth, SectionHeight)
            ).Centered(BackgroundColor);
        }

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            _selectionMade = false;

            while (!_selectionMade)
            {
                Console.SetCursorPosition(0, 0);

                RefreshUntilKeyAvailable();

                var input = Console.ReadKey(true).Key;

                CheckInput(input);

                Console.SetCursorPosition(0, 0);

                Print(GetDisplay());

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }

            var selectedSettings = new ExerciseSettingsBuilder()
                .SetLives(_startingLives, ExerciseSettingConstant.MaxLives)
                .SetMinimumKeysPerSecond(_minWordsPerMinute)
                .SetExerciseTimeLimit(_exerciseTimerInSeconds)
                .Build();

            return _funcGetExerciseWithSelectedSettings.Invoke(selectedSettings);
        }

        private void CheckInput(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.Enter:
                    _selectionMade = _currentIndex == _startExerciseSectionIndex;
                break;

                // 0 goes to max, everything else goes down
                case ConsoleKey.UpArrow:
                    _currentIndex = _currentIndex == 0 ? _maxIndex : --_currentIndex;
                break;

                // max goes to 0, everything else goes up
                case ConsoleKey.DownArrow:
                    _currentIndex = _currentIndex == _maxIndex ? 0 : ++_currentIndex;
                break;

                case ConsoleKey.LeftArrow:
                    DecreaseCurrentSetting();
                break;

                case ConsoleKey.RightArrow:
                    IncreaseCurrentSetting();
                break;

                default: break;
            }
        }

        private void DecreaseCurrentSetting()
        {            
            if (_currentIndex == _liveSectionIndex)
            {
                if (_startingLives > 0)
                {
                    _startingLives--;
                }
            }

            if (_currentIndex == _minWordsPerMinuteSectionIndex)
            {
                if (_minWordsPerMinute.GetValueOrDefault() <= 1)
                {
                    _minWordsPerMinute = null;
                }
                else
                {
                    _minWordsPerMinute--;
                }
            }

            if (_currentIndex == _timeLimitSectionIndex)
            {
                if (_exerciseTimerInSeconds.GetValueOrDefault() <= ExerciseSettingConstant.MinExerciseTimerInSeconds)
                {
                    _exerciseTimerInSeconds = null;
                }
                else
                {
                    _exerciseTimerInSeconds--;
                }                
            }
        }

        private void IncreaseCurrentSetting()
        {
            if (_currentIndex == _liveSectionIndex)
            {
                if (_startingLives < ExerciseSettingConstant.MaxLives)
                {
                    _startingLives++;
                }
            }

            if (_currentIndex == _minWordsPerMinuteSectionIndex)
            {
                if (_minWordsPerMinute.GetValueOrDefault() < ExerciseSettingConstant.MaxWordsPerMinuteRequired)
                {
                    _minWordsPerMinute = _minWordsPerMinute.GetValueOrDefault() + 1;
                }
            }

            if (_currentIndex == _timeLimitSectionIndex)
            {
                if (_exerciseTimerInSeconds == null)
                {
                    _exerciseTimerInSeconds = ExerciseSettingConstant.MinExerciseTimerInSeconds;
                }
                else if(_exerciseTimerInSeconds.Value < ExerciseSettingConstant.MaxExerciseTimerInSeconds)
                {
                    _exerciseTimerInSeconds = _exerciseTimerInSeconds.Value + 1;
                }
            }
        }

        private ConsoleColor GetSectionFontColor(bool isSelectedSection) => isSelectedSection ? PrimaryFontColor : SecondaryFontColor;
    }
}

using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ScrollingMenu : RefreshableScreen
    {
        public abstract (string OptionMessage, Screen NextScreen)[] NextScreenOptions { get; }

        public abstract int CountOfOptionsToShow { get; }

        private int _currentIndex = 0;

        public abstract DisplayedSection FormatSelectedOption(string option);
            
        public abstract DisplayedSection FormatNonSelectedOption(string option);

        public abstract DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions);

        public DisplayedSection GetFormattedOptions()
        {
            var optionMessages = NextScreenOptions.Select(_ => _.OptionMessage);

            IEnumerable<string> optionsBeforeSelectedOption, optionsAfterSelectedOption;

            //TODO: Fix the logical error in here that causes the wrong number of options to be presented (see when more than three options shown)
            if (_currentIndex == 0)
            {
                optionsBeforeSelectedOption = [];
                optionsAfterSelectedOption = optionMessages.Skip(1).Take(CountOfOptionsToShow - 1);
            }
            else if (_currentIndex == optionMessages.Count() - 1)
            {
                optionsBeforeSelectedOption = optionMessages.TakeLast(CountOfOptionsToShow).SkipLast(1);
                optionsAfterSelectedOption = [];
            }
            else
            {
                var firstElementToShow = _currentIndex - (CountOfOptionsToShow / 2);

                optionsBeforeSelectedOption = optionMessages.Skip(firstElementToShow).Take(CountOfOptionsToShow / 2);

                var countOfOptionsBeforeSelectedOption = optionsBeforeSelectedOption.Count();

                optionsAfterSelectedOption = optionMessages.Skip(_currentIndex + 1).Take(CountOfOptionsToShow - (countOfOptionsBeforeSelectedOption + 1));
            }

            var displayedSection = new DisplayedSection();

            if (optionsBeforeSelectedOption.Any())
            {
                foreach (var optionSection in optionsBeforeSelectedOption.Select(FormatNonSelectedOption))
                {
                    displayedSection.AddRange(optionSection);
                }
            }

            displayedSection.AddRange(FormatSelectedOption(optionMessages.ElementAt(_currentIndex)));

            if (optionsAfterSelectedOption.Any())
            {
                foreach (var optionSection in optionsAfterSelectedOption.Select(FormatNonSelectedOption))
                {
                    displayedSection.AddRange(optionSection);
                }
            }

            return displayedSection;
        }

        public override DisplayedSection GetDisplay() => GetDisplayedContent(GetFormattedOptions());

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            var maxIndex = NextScreenOptions.Length - 1;

            var selectionMade = false;

            while (!selectionMade)
            {
                Console.SetCursorPosition(0, 0);

                RefreshUntilKeyAvailable();

                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.Enter:
                        selectionMade = true;
                        break;

                    // 0 goes to max, everything else goes down
                    case ConsoleKey.UpArrow:
                        _currentIndex = _currentIndex == 0 ? maxIndex : --_currentIndex;
                        break;

                    // max goes to 0, everything else goes up
                    case ConsoleKey.DownArrow:
                        _currentIndex = _currentIndex == maxIndex ? 0 : ++_currentIndex;
                        break;

                    default:
                        break;
                }
            }

            return NextScreenOptions[_currentIndex].NextScreen;
        }
    }
}

using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ScrollingMenu : Screen
    {
        public abstract IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions();

        public abstract int CountOfOptionsToShow { get; }

        public abstract DisplayedSection FormatSelectedOption(string option);
            
        public abstract DisplayedSection FormatNonSelectedOption(string option);

        public abstract DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions);

        public DisplayedSection GetFormattedOptions(IEnumerable<string> optionMessages, int currentIndex)
        {
            IEnumerable<string> optionsBeforeSelectedOption, optionsAfterSelectedOption;

            //TODO: Fix the logical error in here that causes the wrong number of options to be presented (see when more than three options shown)
            if (currentIndex == 0)
            {
                optionsBeforeSelectedOption = [];
                optionsAfterSelectedOption = optionMessages.Skip(1).Take(CountOfOptionsToShow - 1);
            }
            else if (currentIndex == optionMessages.Count() - 1)
            {
                optionsBeforeSelectedOption = optionMessages.TakeLast(CountOfOptionsToShow).SkipLast(1);
                optionsAfterSelectedOption = [];
            }
            else
            {
                var firstElementToShow = currentIndex - (CountOfOptionsToShow / 2);

                optionsBeforeSelectedOption = optionMessages.Skip(firstElementToShow).Take(CountOfOptionsToShow / 2);

                var countOfOptionsBeforeSelectedOption = optionsBeforeSelectedOption.Count();

                optionsAfterSelectedOption = optionMessages.Skip(currentIndex + 1).Take(CountOfOptionsToShow - (countOfOptionsBeforeSelectedOption + 1));
            }

            var displayedSection = new DisplayedSection();

            if (optionsBeforeSelectedOption.Any())
            {
                foreach (var optionSection in optionsBeforeSelectedOption.Select(FormatNonSelectedOption))
                {
                    displayedSection.AddRange(optionSection);
                }
            }

            displayedSection.AddRange(FormatSelectedOption(optionMessages.ElementAt(currentIndex)));

            if (optionsAfterSelectedOption.Any())
            {
                foreach (var optionSection in optionsAfterSelectedOption.Select(FormatNonSelectedOption))
                {
                    displayedSection.AddRange(optionSection);
                }
            }

            return displayedSection;
        }

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            var options = GetOptions();
            var optionMessages = options.Select(_ => _.OptionMessage);
            var screens = options.Select(_ => _.NextScreen);

            var currentIndex = 0;
            var maxIndex = options.Count() - 1;
            var selectionMade = false;

            while (!selectionMade)
            {
                Console.SetCursorPosition(0, 0);

                var formattedOptions = GetFormattedOptions(optionMessages, currentIndex);

                new DisplayedScreen(GetDisplayedContent(formattedOptions)).Write();

                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.Enter:
                        selectionMade = true;
                        break;

                    // 0 goes to max, everything else goes down
                    case ConsoleKey.UpArrow:
                        currentIndex = currentIndex == 0 ? maxIndex : --currentIndex;
                        break;

                    // max goes to 0, everything else goes up
                    case ConsoleKey.DownArrow:
                        currentIndex = currentIndex == maxIndex ? 0 : ++currentIndex;
                        break;

                    default:
                        break;
                }
            }

            return options.ElementAt(currentIndex).NextScreen;
        }
    }
}

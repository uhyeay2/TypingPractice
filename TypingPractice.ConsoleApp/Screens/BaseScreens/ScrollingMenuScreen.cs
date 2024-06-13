using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ScrollingMenuScreen : Screen
    {
        public abstract IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions();

        public abstract int CountOfOptionsToShow { get; }

        public abstract IEnumerable<DisplayedLine> FormatSelectedOption(string option);
            
        public abstract IEnumerable<DisplayedLine> FormatNonSelectedOption(string option);

        public abstract DisplayedContent GetDisplayedContent(IEnumerable<DisplayedLine> formattedOptions);

        public IEnumerable<DisplayedLine> GetFormattedOptions(IEnumerable<string> optionMessages, int currentIndex)
        {
            IEnumerable<string> optionsBeforeSelectedOption, optionsAfterSelectedOption;

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

            var lines = new List<DisplayedLine>();

            if (optionsBeforeSelectedOption.Any())
            {
                lines.AddRange(optionsBeforeSelectedOption.Select(FormatNonSelectedOption).Aggregate((a, b) => a.Concat(b)));
            }

            lines.AddRange(FormatSelectedOption(optionMessages.ElementAt(currentIndex)));

            if (optionsAfterSelectedOption.Any())
            {
                lines.AddRange(optionsAfterSelectedOption.Select(FormatNonSelectedOption).Aggregate((a, b) => a.Concat(b)));
            }

            return lines;
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

                GetDisplayedContent(formattedOptions).Print();

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

using TypingPractice.ConsoleApp.Constants;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.Settings
{
    public class UpdateConsoleSizeScreen : ScrollingMenu
    {
        private readonly Screen _nextScreen;

        private const string ContinueMessage = "Continue";
        private const string FullScreenMessage = "Use Full Screen";
        private const string MinRequirementsMessage = "Use Minimum Requirements";

        public UpdateConsoleSizeScreen(Screen nextScreen) => _nextScreen = nextScreen;

        private (string OptionMessage, Screen NextScreen)[] DontContinueOptions =>
        [
            (FullScreenMessage, this),
            (MinRequirementsMessage, this)
        ];

        private (string OptionMessage, Screen NextScreen)[] ContinueOptions =>
        [
            (ContinueMessage, _nextScreen),
            (FullScreenMessage, this),
            (MinRequirementsMessage, this)
        ];

        public override (string OptionMessage, Screen NextScreen)[] NextScreenOptions =>
            ConsoleSize.IsMeetingMinimumRequirements ? ContinueOptions : DontContinueOptions;

        private static ConsoleColor CurrentSizeColor(int currentSize, int requiredSize) =>
            currentSize >= requiredSize ? ConsoleColor.Green : ConsoleColor.Red;

        public override DisplayedSection FormatNonSelectedOption(string option) => new (SecondaryFontColor, BackgroundColor, $"{option}    ");

        public override DisplayedSection FormatSelectedOption(string option) => new (SecondaryFontColor, BackgroundColor, $"{option} <--");

        // Override the GetFormattedOptions to wrap in a try/catch since the number of options can change at any time based on if the console size changes.
        // If the number of options goes from 3 to 2, and the user is on the last index, this will ensure the application does not break and the index is set back to 0
        public override DisplayedSection GetFormattedOptions()
        {
            try
            {
                return base.GetFormattedOptions();
            }
            catch (Exception)
            {
                _currentIndex = 0;

                return [];
            }
        }

        public override DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions)
        {            
            var display = new DisplayedSection(
                new DisplayedSection(PrimaryFontColor, BackgroundColor,
                    "Thank you for playing Typing Practice!",
                    "",
                    "To keep playing, your console must meet minimum size requirements.",
                    ""),
                new DisplayedSection(SecondaryFontColor, BackgroundColor,
                    $"Minimum Width: {ConsoleSize.MinimumWidth}",
                    $"Minimum Height: {ConsoleSize.MinimumHeight}",
                    ""),
                new DisplayedSection(CurrentSizeColor(Console.WindowWidth, ConsoleSize.MinimumWidth), BackgroundColor,
                    $"Current Width: {Console.WindowWidth}"),
                new DisplayedSection(CurrentSizeColor(Console.WindowHeight, ConsoleSize.MinimumHeight), BackgroundColor,
                    $"Current Height: {Console.WindowHeight}",
                    ""),
                formattedOptions);

            try
            {
                return display.Centered(BackgroundColor);
            }
            catch (Exception)
            {
                return display;
            }
        }

        public override Screen GetNextScreen()
        {            
            var selectedOption = NextScreenOptions[_currentIndex];

            // set CurrentIndex to Zero so that 'Continue' option will be selected on next refresh
            _currentIndex = 0;

            if (selectedOption.OptionMessage == ContinueMessage && ConsoleSize.IsMeetingMinimumRequirements)
            {
                return selectedOption.NextScreen;
            }

            if (selectedOption.OptionMessage == FullScreenMessage)
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            }
            else if (selectedOption.OptionMessage == MinRequirementsMessage)
            {
                Console.SetWindowSize(ConsoleSize.MinimumWidth, ConsoleSize.MinimumHeight);
            }
            
            return this;
        }
    }
}

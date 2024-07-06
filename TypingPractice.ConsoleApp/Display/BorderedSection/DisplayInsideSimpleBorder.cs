using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.BorderedSection
{
    public class DisplayInsideSimpleBorder : DisplayedSection
    {
        private readonly ConsoleColor _borderColor;
        private readonly ConsoleColor _backgroundColor;

        private const int _borderWidth = 4;
        private const int _borderHeight = 2;

        public DisplayInsideSimpleBorder(ConsoleColor borderColor, ConsoleColor backgroundColor, int width, int height, params DisplayedSection[] contentInsideBorder)
        {
            _borderColor = borderColor;
            _backgroundColor = backgroundColor;

            var content = new DisplayedSection(contentInsideBorder).Centered(backgroundColor, width - _borderWidth, height - _borderHeight);

            // add top border
            Add(BorderedLine(" _", GetMiddleSection('_', width - _borderWidth), "_ "));

            foreach (var line in content)
            {
                // add content inside border with left and right side border added
                Add(LeftBorder + line + RightBorder);
            }

            // Add Bottom border
            Add(BorderedLine("|_", GetMiddleSection('_', width - _borderWidth), "_|"));
        }

        private DisplayedLine LeftBorder => new("| ", _borderColor, _backgroundColor);

        private DisplayedLine RightBorder => new (" |", _borderColor, _backgroundColor);

        private DisplayedLine GetMiddleSection(char character, int width) => new(character, width, _borderColor, _backgroundColor);

        private DisplayedLine BorderedLine(string leftBorder, DisplayedLine middle, string rightBorder) =>
            new DisplayedLine(leftBorder, _borderColor, _backgroundColor) + middle + new DisplayedLine(rightBorder, _borderColor, _backgroundColor);
    }
}

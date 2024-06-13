using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.BorderedSection
{
    internal class BasicBorder : DisplayedBorder
    {
        public BasicBorder(ConsoleColor fontColor, ConsoleColor backgroundColor, int width, int height, params DisplayedSection[] content) 
        : this(fontColor, backgroundColor, width, height, new DisplayedSection(content)) { }

        private BasicBorder(ConsoleColor fontColor, ConsoleColor backgroundColor, int width, int height, DisplayedSection content) : base(width, height, content)
        {
            _fontColor = fontColor;

            _backgroundColor = backgroundColor;
        }

        private readonly ConsoleColor _fontColor;

        private readonly ConsoleColor _backgroundColor;

        private const int _borderWidth = 4;
        private const int _borderHeight = 2;

        protected override DisplayedSection GetTopBorder() => 
            new(BorderedLine(" _", GetMiddleSection('_', _width - _borderWidth), "_ "));

        protected override DisplayedSection GetBottomBorder() => 
            new(BorderedLine("|_", GetMiddleSection('_', _width - _borderWidth), "_|"));

        private DisplayedLine LeftBorder => new("| ", _fontColor, _backgroundColor);
        private DisplayedLine RightBorder => new (" |", _fontColor, _backgroundColor);

        protected override DisplayedSection GetContentWithSideBorders()
        {
            var borderedContents = new DisplayedSection();

            var topPaddingSize = (_height - _borderHeight - _insideSection.Count) / 2;

            while (borderedContents.Count < topPaddingSize)
            {
                borderedContents.Add(BorderedLine("| ", GetMiddleSection(' ', _width - _borderWidth), " |"));
            }

            foreach (var line in _insideSection.CenteredHorizontal(_backgroundColor, _width - _borderWidth))
            {
                borderedContents.Add(LeftBorder + line + RightBorder);
            }

            while (borderedContents.Count < (_height - _borderHeight))
            {
                borderedContents.Add(BorderedLine("| ", GetMiddleSection(' ', _width - _borderWidth), " |"));
            }

            return borderedContents;
        }

        private DisplayedLine GetMiddleSection(char character, int width) => new (character, width , _fontColor, _backgroundColor);

        private DisplayedLine BorderedLine(string leftBorder, DisplayedLine middle, string rightBorder) => 
            new DisplayedLine(leftBorder, _fontColor, _backgroundColor) + middle + new DisplayedLine(rightBorder, _fontColor, _backgroundColor);
    }
}

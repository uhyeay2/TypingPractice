using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.BorderedSection
{
    public abstract class DisplayedBorder
    {
        protected readonly DisplayedSection _insideSection;

        protected readonly int _width;

        protected readonly int _height;

        protected DisplayedBorder(int width, int height, DisplayedSection insideSection)
        {
            _width = width;

            _height = height;

            _insideSection = insideSection;
        }

        public DisplayedSection ToDisplayedSection() => new(GetTopBorder(), GetContentWithSideBorders(), GetBottomBorder());

        protected abstract DisplayedSection GetTopBorder();

        protected abstract DisplayedSection GetContentWithSideBorders();

        protected abstract DisplayedSection GetBottomBorder();
    }
}

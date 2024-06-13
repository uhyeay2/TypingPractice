
using Figgle;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    public class DisplayedSection : List<DisplayedLine>
    {
        public DisplayedSection() { }

        public DisplayedSection(ConsoleColor fontColor, ConsoleColor backgroundColor, FiggleFont font, params string[] text)             
        {
            foreach (var t in text)
            {
                AddRange(new DisplayedSection(fontColor, backgroundColor, font, t));
            }
        }

        public DisplayedSection(ConsoleColor fontColor, ConsoleColor backgroundColor, FiggleFont font, string text)
            : this(fontColor, backgroundColor, font.RenderIEnumerable(text).ToArray()) { }

        public DisplayedSection(ConsoleColor fontColor, ConsoleColor backgroundColor, params string[] linesToDisplay)
        {
            foreach (var line in linesToDisplay)
            {
                Add(new (line, fontColor, backgroundColor));
            }
        }

        public DisplayedSection(params DisplayedLine[] displayedLines)
        {
            AddRange(displayedLines);   
        }

        public DisplayedSection(params List<DisplayedLine>[] displayedLines)
        {
            foreach (var section in displayedLines)
            {
                AddRange(section);
            }
        }

        public DisplayedSection Centered(ConsoleColor backgroundColor) => Centered(backgroundColor, Console.WindowWidth, Console.WindowHeight - 1);

        public DisplayedSection Centered(ConsoleColor backgroundColor, int width, int height) =>
            CenteredHorizontal(backgroundColor, width)
            .CenteredVertical(backgroundColor, height);

        public DisplayedSection CenteredVertical(ConsoleColor backgroundColor) => CenteredVertical(backgroundColor, Console.WindowHeight - 1);

        public DisplayedSection CenteredVertical(ConsoleColor backgroundColor, int height)
        {
            if (Count >= height)
            {
                return this;
            }

            var maxWidth = this.Max(_ => _.Width);

            var paddingLine = new DisplayedLine(' ', maxWidth, backgroundColor, backgroundColor);

            while (Count < height)
            {
                Add(paddingLine);

                if (Count < height)
                {
                    Insert(0, paddingLine);
                }
            }

            return this;
        }

        public DisplayedSection CenteredHorizontal(ConsoleColor backgroundColor) => CenteredHorizontal(backgroundColor, Console.WindowWidth);

        public DisplayedSection CenteredHorizontal(ConsoleColor backgroundColor, int width)
        {
            foreach (var line in this)
            {
                var leftPaddingSize = (width - line.Width) / 2;

                var rightPaddingSize = width - line.Width - leftPaddingSize;

                line.Insert(0, new(' ', leftPaddingSize, backgroundColor, backgroundColor));

                line.Add(new(' ', rightPaddingSize, backgroundColor, backgroundColor));
            }

            return this;
        }
    }
}

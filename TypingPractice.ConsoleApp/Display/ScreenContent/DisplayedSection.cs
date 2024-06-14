using Figgle;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    public class DisplayedSection : List<DisplayedLine>
    {
        #region Constructors 

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
                Add(new DisplayedLine(line, fontColor, backgroundColor));
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

        #endregion

        #region Methods For Centering Displayed Section

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

            while (Count < height)
            {
                Add(new DisplayedLine(' ', maxWidth, backgroundColor, backgroundColor));

                if (Count < height)
                {
                    Insert(0, new DisplayedLine(' ', maxWidth, backgroundColor, backgroundColor));
                }
            }

            return this;
        }

        public DisplayedSection CenteredHorizontal(ConsoleColor backgroundColor) => CenteredHorizontal(backgroundColor, Console.WindowWidth);

        public DisplayedSection CenteredHorizontal(ConsoleColor backgroundColor, int width)
        {
            foreach (var line in this)
            {
                if (line.Width > width)
                {
                    var offendingString = new string(line.Select(_ => _.Value).Aggregate((a, b) => a += b));

                    throw new ArgumentOutOfRangeException($"Cannot Center DisplayedSection. Line exceeds width {width} - Offending Line ({offendingString.Length}): {offendingString}");
                }

                var leftPaddingSize = (width - line.Width) / 2;

                var rightPaddingSize = width - line.Width - leftPaddingSize;

                line.Insert(0, new(' ', leftPaddingSize, backgroundColor, backgroundColor));

                line.Add(new(' ', rightPaddingSize, backgroundColor, backgroundColor));
            }

            return this;
        }

        #endregion
    
        public DisplayedSection AddRightSideSection(ConsoleColor paddingColor, DisplayedSection rightSection)
        {
            if (Count < rightSection.Count)
            {
                CenteredVertical(paddingColor, rightSection.Count);
            }
            else if (rightSection.Count < Count)
            {
                rightSection = rightSection.CenteredVertical(paddingColor, Count);
            }

            for (int i = 0; i < Count; i++)
            {
                this[i] += rightSection.ElementAt(i);
            }

            return this;
        }
    }
}

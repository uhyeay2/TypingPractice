using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Borders
{
    public abstract class BaseBorder
    {
        protected BaseBorder(ConsoleColor backgroundColor) => BackgroundColor = backgroundColor;

        public ConsoleColor BackgroundColor { get; set; }

        public abstract IEnumerable<DisplayedLine> GetTopBorder(int targetWidth);

        public abstract IEnumerable<DisplayedLine> GetBottomBorder(int targetWidth);

        public abstract IEnumerable<DisplayedLine> AddSideBorders(int targetWidth, IEnumerable<DisplayedLine> content);

        public virtual IEnumerable<DisplayedLine> PadContentInsideBorder(int targetWidth, int? targetHeight, IEnumerable<DisplayedLine> content) =>
            content.PadToCenter(BackgroundColor, targetWidth);
          
        public abstract int WidthOfSideBorders { get; }

        public IEnumerable<DisplayedLine> Apply(params IEnumerable<DisplayedLine>[] content) => Apply(targetWidth: null, strictWidth: false, targetHeightInsideBorders: null, content);

        public IEnumerable<DisplayedLine> Apply(int? targetWidth, bool strictWidth, int? targetHeightInsideBorders, params IEnumerable<DisplayedLine>[] content)
        {
            var displayedLines = content.Aggregate((a, b) => a.Concat(b)).ToArray();

            var borderWidth = targetWidth.GetValueOrDefault();

            var lengthOfLongestLine = displayedLines.Max(_ => _.Width);

            if (lengthOfLongestLine >= borderWidth - WidthOfSideBorders)
            {
                if (strictWidth)
                {
                    var offendingLines = displayedLines.Where(_ => _.Width >= borderWidth - WidthOfSideBorders);

                    throw new ArgumentOutOfRangeException($"Border Strict Width ({targetWidth}) Exceeded. Content has {offendingLines.Count()} lines that are too long.");
                }

                borderWidth = lengthOfLongestLine + WidthOfSideBorders;
            }

            return GetTopBorder(borderWidth).Concat(
                   AddSideBorders(borderWidth, PadContentInsideBorder(borderWidth, targetHeightInsideBorders.GetValueOrDefault(), displayedLines))).Concat(
                   GetBottomBorder(borderWidth));
        }
    }
}

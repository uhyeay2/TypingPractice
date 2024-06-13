using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Extensions
{
    public static class IEnumerableDisplayedLineExtensions
    {
        public static IEnumerable<DisplayedLine> MergeOnRightSide(this IEnumerable<DisplayedLine> source, IEnumerable<DisplayedLine> contentToMerge, ConsoleColor paddingColor = ConsoleColor.Black)
        {
            var newContentHeight = contentToMerge.Count();
            var sourceContentHeight = source.Count();

            int targetHeight = newContentHeight > sourceContentHeight ? newContentHeight : sourceContentHeight;

            if (targetHeight > sourceContentHeight)
            {
                var longestLine = source.Max(_ => _.Width);

                source = source.PadTopAndBottom(targetHeight, new DisplayedText(new string(' ', longestLine), paddingColor, paddingColor));
            }

            if (targetHeight > newContentHeight)
            {
                var longestLine = contentToMerge.Max(_ => _.Width);

                contentToMerge = contentToMerge.PadTopAndBottom(targetHeight, new DisplayedText(new string(' ', longestLine), paddingColor, paddingColor));
            }

            // pad both source and new content so that they are the same height.
            //var paddedSource = source.PadTopAndBottom(padding, targetHeight);
            //var paddedContentToMerge = contentToMerge.PadTopAndBottom(padding, targetHeight);

            // return the source with contentToMerge appended to the right, will padding appended first if it is not null.
            return source.Select((x, i) =>
            {
                return x.Append(contentToMerge.ElementAt(i));
            }).ToArray();
        }

        public static IEnumerable<DisplayedLine> PadTopAndBottom(this IEnumerable<DisplayedLine> source, int targetHeight, DisplayedText? padding = null)
        {
            var enumeratedSource = source.ToArray();

            var currentHeight = enumeratedSource.Count();

            if (currentHeight >= targetHeight)
            {
                return enumeratedSource;
            }

            var topPaddingSize = (int) Math.Ceiling((targetHeight - currentHeight) / 2.00);

            var bottomPaddingSize = targetHeight - (topPaddingSize + currentHeight) - 1;

            return enumeratedSource.PadTop(topPaddingSize, padding).PadBottom(bottomPaddingSize, padding);
        }

        public static IEnumerable<DisplayedLine> PadTop(this IEnumerable<DisplayedLine> source, int countOfPaddingLines, DisplayedText? padding = null)
        {
            if (countOfPaddingLines == 0)
            {
                return source;
            }

            var enumeratedSource = source.ToArray();

            var topPadding = Enumerable.Repeat(new DisplayedLine(padding ?? new(' ', ConsoleColor.Black, ConsoleColor.Black)), countOfPaddingLines);

            var result = topPadding.Concat(enumeratedSource);

            return result;

        }

        public static IEnumerable<DisplayedLine> PadBottom(this IEnumerable<DisplayedLine> source, int countOfPaddingLines, DisplayedText? padding = null)
        {
            if (countOfPaddingLines <= 0)
            {
                return source;
            }

            var enumeratedSource = source.ToArray();

            var bottomPadding = Enumerable.Repeat(new DisplayedLine(padding ?? new(' ', ConsoleColor.Black, ConsoleColor.Black)), countOfPaddingLines);

            var result = enumeratedSource.Concat(bottomPadding);

            return result;
        }

        #region Pad Horizontal

        public static IEnumerable<DisplayedLine> PadToCenterOfConsole(this IEnumerable<DisplayedLine> lines, ConsoleColor paddingColor) =>
            lines.PadToCenter(paddingColor, Console.WindowWidth, strictWidth: true);

        public static IEnumerable<DisplayedLine> PadToCenter(this IEnumerable<DisplayedLine> lines, ConsoleColor paddingColor, int? targetWidth = null, bool strictWidth = false) =>
            lines.PadToCenter(' ', paddingColor, paddingColor, targetWidth, strictWidth);

        public static IEnumerable<DisplayedLine> PadToCenter(this IEnumerable<DisplayedLine> lines, char padding, ConsoleColor paddingColor, ConsoleColor paddingBackgroundColor, int? targetWidth = null, bool strictWidth = false)
        {
            var enumeratedLines = lines.ToArray();
          
            var width = targetWidth.GetValueOrDefault();

            var widthOfLongestLine = enumeratedLines.Max(_ => _.Width);

            if (widthOfLongestLine > width)
            {
                width = !strictWidth ? widthOfLongestLine :
                    throw new ArgumentOutOfRangeException($"Content Exceeding Strict Target Width. Width Of Longest Line: {widthOfLongestLine} - Target Width: {width}");
            }

            return enumeratedLines.Select(_ =>
            {
                var currentWidth = _.Width;

                var leftPaddingSize = (width - currentWidth) / 2;

                var rightPaddingSize = width - currentWidth - leftPaddingSize;

                return _.Prepend(new DisplayedText( new string(padding, leftPaddingSize), paddingColor, paddingBackgroundColor))
                        .Append(new DisplayedText( new string(padding, rightPaddingSize), paddingColor, paddingBackgroundColor));
            });
        }

        #endregion
    }
}

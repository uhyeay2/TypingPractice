using Figgle;

namespace TypingPractice.ConsoleApp.Extensions
{
    public static class FiggleFontExtensions
    {
        public static IEnumerable<string> RenderIEnumerable(this FiggleFont font, int targetHeight, params string[] content)
        {
            var figgledContent = RenderIEnumerable(font, content);

            var currentHeight = figgledContent.Count();

            if (currentHeight < targetHeight)
            {
                var topPaddingSize = (targetHeight - currentHeight) / 2;
                var bottomPaddingSize = targetHeight - (currentHeight + topPaddingSize);

                return Enumerable.Range(0, topPaddingSize).Select(_ => "")
                                 .Concat(figgledContent)
                                 .Concat(Enumerable.Range(0, bottomPaddingSize).Select(_ => ""));
            }

            return figgledContent;
        }

        public static IEnumerable<string> RenderIEnumerable(this FiggleFont font, params string[] content)
        {
            return content.Select(_ => font.RenderIEnumerable(_)).Aggregate((a, b) => a.Concat(b));
        }

        public static IEnumerable<string> RenderIEnumerable(this FiggleFont font, string content, int? targetHeight = null)
        {
            // Return Blank Line when content to apply FiggleFont to is null/empty/whitespace.
            if (string.IsNullOrWhiteSpace(content))
            {
                return [""];
            }

            // FiggleFont renders as a single string with extra whiteSpace at the end, this splits it into separate lines
            var figgledLines = font.Render(content).Split("\r\n");

            // Then remove trailing whitespace lines from Figgle
            var countOfTrailingWhiteSpace = 0;

            for (int i = figgledLines.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(figgledLines.ElementAt(i)))
                {
                    countOfTrailingWhiteSpace++;
                }
                else
                {
                    i = 0;
                }
            }

            List<string> result = [];

            var countOfLinesToKeep = figgledLines.Length - countOfTrailingWhiteSpace;

            if (targetHeight != null && countOfLinesToKeep < targetHeight)
            {
                var topPaddingSize = (targetHeight.Value - countOfLinesToKeep) / 2;

                if (countOfLinesToKeep < targetHeight)
                {
                    result.AddRange(Enumerable.Range(0, topPaddingSize).Select(_ => ""));
                }

                result.AddRange(figgledLines.Take(countOfLinesToKeep));

                var bottomPaddingSize = targetHeight.Value - (countOfLinesToKeep + topPaddingSize);

                if (bottomPaddingSize > 0)
                {
                    result.AddRange(Enumerable.Range(0, bottomPaddingSize).Select(_ => ""));
                }
            }
            else
            {
                result.AddRange(figgledLines.Take(countOfLinesToKeep));
            }

            return result;
        }
    }
}

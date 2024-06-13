using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.UserInterface
{
    public static class Center
    {
        public static IEnumerable<string> ByConsoleWidthAndHeight(IEnumerable<string> content) =>
            Vertical(Horizontal(content, Console.WindowWidth), Console.WindowHeight);

        public static IEnumerable<string> HorizontalByConsoleWidth(params IEnumerable<string>[] content) => 
            Horizontal(content.Aggregate((a, b) => a.Concat(b)), Console.WindowWidth);

        public static IEnumerable<string> Horizontal(IEnumerable<string> content, int? targetLength = null)
        {
            var minLength = content.Max(_ => _.Length);

            var length = targetLength.GetValueOrDefault() < minLength ? minLength : targetLength.GetValueOrDefault();

            return content.Select(_ => _.PadToCenter(length));
        }

        public static IEnumerable<string> Vertical(int targetHeight, params IEnumerable<string>[] content) =>
            Vertical(content.Aggregate((a, b) => a.Concat(b)), targetHeight);

        public static IEnumerable<string> Vertical(IEnumerable<string> content, int targetHeight)
        {
            var currentHeight = content.Count() - 1;

            if (currentHeight >= targetHeight)
            {
                return content;
            }

            var topPaddingSize = (targetHeight - content.Count()) / 2;

            var bottomPaddingSize = targetHeight - (topPaddingSize + currentHeight);

            var topPadding = Enumerable.Range(0, topPaddingSize).Select(_ => string.Empty);

            var bottomPadding = Enumerable.Range(0, bottomPaddingSize).Select(_ => string.Empty);

            return topPadding.Concat(content).Concat(bottomPadding);
        }
    }
}

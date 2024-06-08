using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.UserInterface
{
    public static class Border
    {
        private const int PaddingForExpandingBorderWidth = 6;
        
        private const int WidthOfBorderSides = 4;

        public static IEnumerable<string> Apply(params string[] content) => Apply(content, null);

        public static IEnumerable<string> Apply(int? targetWidth, params string[] content) => Apply(content, targetWidth);
        
        public static IEnumerable<string> Apply(string content, int? targetWidth = null) => Apply(targetWidth, content);

        public static IEnumerable<string> Apply(IEnumerable<string> content, int? targetWidth = null)
        {
            var borderWidth = targetWidth.GetValueOrDefault();

            var lengthOfLongestLine = content.Max(_ => _.Length);

            if (lengthOfLongestLine > borderWidth)
            {
                borderWidth = lengthOfLongestLine + PaddingForExpandingBorderWidth;
            }

            var topBorder = new[]
            {
                " _" + new string('_', borderWidth - WidthOfBorderSides) + "_ ",
                "| " + new string(' ', borderWidth - WidthOfBorderSides) + " |"
            };

            var innerLines = content.Select(_ =>
                "| " + _.PadToCenter(borderWidth - WidthOfBorderSides) + " |");

            var bottomBorder = new[]
            {
                "|_" + new string('_', borderWidth - WidthOfBorderSides) + "_|"
            };

            return topBorder.Concat(innerLines).Concat(bottomBorder);
        }
    }
}

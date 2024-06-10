using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.UserInterface
{
    public static class Border
    {
        #region Constants For Creating Border

        private const int PaddingForExpandingBorderWidth = 6;
        
        private const int WidthOfBorderSides = 4;

        #endregion

        #region Apply Border To One Or More IEnumerable<String>

        public static IEnumerable<string> Apply(params IEnumerable<string>[] content) => Apply(targetWidth: null, content);

        public static IEnumerable<string> Apply(int? targetWidth = null, params IEnumerable<string>[] content) => Apply(content.Aggregate((a,b) => a = a.Concat(b)), targetWidth);

        #endregion

        #region Apply Border To One Or More Strings

        public static IEnumerable<string> Apply(params string[] content) => Apply(content, targetWidth: null);

        public static IEnumerable<string> Apply(int? targetWidth, params string[] content) => Apply(content, targetWidth);

        #endregion

        #region Apply Border

        private static IEnumerable<string> Apply(IEnumerable<string> content, int? targetWidth = null)
        {
            var borderWidth = targetWidth.GetValueOrDefault();

            var lengthOfLongestLine = content.Max(_ => _.Length);

            if (lengthOfLongestLine > borderWidth - WidthOfBorderSides)
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

        #endregion
    }
}

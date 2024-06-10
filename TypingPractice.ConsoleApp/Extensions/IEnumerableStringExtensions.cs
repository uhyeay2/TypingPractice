using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Extensions
{
    public static class IEnumerableStringExtensions
    {
        public static IEnumerable<string> MergeToRight(this IEnumerable<string> source, IEnumerable<string> contentToMerge, string middlePadding = "")
        {
            var newContentHeight = contentToMerge.Count() - 1;
            var sourceContentHeight = source.Count() - 1;

            int targetHeight;

            if (newContentHeight > sourceContentHeight)
            {
                // new content is longer, pad old content
                targetHeight = newContentHeight;
                source = Center.Vertical(source, targetHeight);
            }
            else if (newContentHeight < sourceContentHeight)
            {
                // old content is longer, pad new content
                targetHeight = sourceContentHeight;
                contentToMerge = Center.Vertical(contentToMerge, targetHeight);
            }
            else
            {
                targetHeight = sourceContentHeight;
            }

            // Ensure that the contents being merged is all the same width to avoid formatting issues.
            contentToMerge = Center.Horizontal(contentToMerge, contentToMerge.Max(_ => _.Length));
            source = Center.Horizontal(source, source.Max(_ => _.Length));

            return source.Select((x, i) => x = x + middlePadding + contentToMerge.ElementAt(i));
        }

    }
}

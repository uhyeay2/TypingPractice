namespace TypingPractice.ConsoleApp.UserInterface
{
    public class ScreenContentToBeBuilt
    {
        public ScreenContentToBeBuilt() { }

        private IEnumerable<string> _contents = [];

        #region Adding Content Methods

        public ScreenContentToBeBuilt AddContent(params string[] content)
        {
            _contents = _contents.Concat(content);

            return this;
        }

        public ScreenContentToBeBuilt AddContent(params IEnumerable<string>[] content)
        {
            foreach (var c in content)
            {
                _contents = _contents.Concat(c);
            }

            return this;
        }

        #endregion

        #region Merging ScreenContent Methods

        public ScreenContentToBeBuilt MergeRightSideContent(Func<ScreenContentToBeBuilt, ScreenContentToBeBuilt> funcGetSideScreenContent, string middlePadding = "")
        {
            var contentToMerge = funcGetSideScreenContent.Invoke(new()).Build().GetContents();

            var newContentHeight = contentToMerge.Count() - 1;
            var currentContentsHeight = _contents.Count() - 1;
            int targetHeight;

            if (newContentHeight > currentContentsHeight)
            {
                // new content is longer, pad old content
                targetHeight = newContentHeight;
                _contents = Center.Vertical(_contents, targetHeight);
            }
            else if (newContentHeight < currentContentsHeight)
            {
                // old content is longer, pad new content
                targetHeight = currentContentsHeight;
                contentToMerge = Center.Vertical(contentToMerge, targetHeight);
            }
            else
            {
                targetHeight = currentContentsHeight;
            }

            // Ensure that the contents being merged is all the same width to avoid formatting issues.
            contentToMerge = Center.Horizontal(contentToMerge, contentToMerge.Max(_ => _.Length));
            _contents = Center.Horizontal(_contents, _contents.Max(_ => _.Length));

            _contents = _contents.Select((x, i) => x = x + middlePadding + contentToMerge.ElementAt(i));

            return this;
        }
      
        #endregion

        public ScreenContent Build() => new(_contents);
    }
}

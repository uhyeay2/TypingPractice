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

        public ScreenContent Build() => new(_contents);
    }
}

namespace TypingPractice.ConsoleApp.UserInterface
{
    public class ScreenContent
    {
        private readonly IEnumerable<string> _content;

        public ScreenContent(IEnumerable<string> content) => _content = content;

        public IEnumerable<string> GetContents() => _content;
    }
}

namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    public class DisplayedScreen
    {
        private readonly List<DisplayedLine> _lines = [];

        public DisplayedScreen(DisplayedSection content) => _lines = content;

        public DisplayedScreen(params DisplayedSection[] content)
        {
            foreach (var section in content)
            {
                _lines.AddRange(section);
            }
        }

        public void ClearThenWrite()
        {
            Console.Clear();

            Write();
        }

        public void Write()
        {
            _lines.ForEach(_ => _.Write());
        }
    }
}

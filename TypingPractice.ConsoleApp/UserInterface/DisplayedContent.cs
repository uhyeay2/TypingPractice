namespace TypingPractice.ConsoleApp.UserInterface
{
    public class DisplayedContent
    {
        #region Private Members

        public IEnumerable<DisplayedLine> ScreenLines { get; set; } = [];

        #endregion

        #region Constructors

        public DisplayedContent() { }

        public DisplayedContent(IEnumerable<DisplayedLine> screenLines) => ScreenLines = screenLines;

        public DisplayedContent(params IEnumerable<DisplayedLine>[] screenLines) => ScreenLines = screenLines.Aggregate((a,b)=> a.Concat(b));

        #endregion
      
        #region Print

        public void ClearAndPrint()
        {
            Console.Clear();

            Print();
        }

        public void Print()
        {
            foreach (var line in ScreenLines)
            {
                foreach (var displayedText in line.Contents)
                {
                    Console.BackgroundColor = displayedText.BackgroundColor;
                    Console.ForegroundColor = displayedText.FontColor;
                    Console.Write(displayedText.Value);
                }
                Console.WriteLine();
            }
        }
        
        #endregion
    }
}

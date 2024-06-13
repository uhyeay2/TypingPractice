namespace TypingPractice.ConsoleApp.UserInterface
{
    public class ConsoleWriter
    {
        public void WriteLines(ScreenContent screenContent)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (var line in screenContent.GetContents())
            {
                Console.WriteLine(line);
            }
        }

        public void ClearThenWriteLines(ScreenContent screenContent)
        {
            Console.Clear();

            WriteLines(screenContent);
        }
    }
}

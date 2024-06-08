namespace TypingPractice.ConsoleApp.UserInterface
{
    public class ConsoleWriter
    {
        public void WriteLines(ScreenContent screenContent)
        {
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

namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    public class DisplayedLine : List<DisplayedString>
    {
        public DisplayedLine(string value, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Add(new(value, fontColor, backgroundColor));
        }

        public DisplayedLine(char value, int count, ConsoleColor fontColor, ConsoleColor backgroundColor) : this(new(value, count), fontColor, backgroundColor) { }

        public int Width => this.Sum(_ => _.Value.Length);

        public void Write()
        {
            ForEach(_ => _.Write());

            Console.WriteLine();
        }

        public static DisplayedLine operator +(DisplayedLine a, DisplayedLine? b)
        {
            if (b == null)
            {
                return a;
            }

            a.AddRange(b);

            return a;
        }
    }
}

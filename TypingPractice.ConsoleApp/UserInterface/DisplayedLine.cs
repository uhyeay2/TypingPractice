namespace TypingPractice.ConsoleApp.UserInterface
{
    public class DisplayedLine
    {
        public DisplayedLine(string value, ConsoleColor fontColor, ConsoleColor backgroundColor) : this([new(value, fontColor, backgroundColor)]) { }

        public DisplayedLine(params DisplayedText[] contents) => Contents = contents;

        public IEnumerable<DisplayedText> Contents { get; set; } = [];

        public int Width => Contents.Sum(_ => _.Value.Length);

        public DisplayedLine Append(DisplayedText text)
        {
            Contents = Contents.Append(text);

            return this;
        }

        public DisplayedLine Append(DisplayedLine line)
        {
            foreach (var text in line.Contents)
            {
                Append(text);
            }

            return this;
        }

        public DisplayedLine Prepend(DisplayedText text)
        {
            Contents = Contents.Prepend(text);

            return this;
        }
    }
}

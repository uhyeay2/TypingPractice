namespace TypingPractice.ConsoleApp.UserInterface
{
    public class DisplayedText
    {
        public DisplayedText(string value, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Value = value;

            FontColor = fontColor;

            BackgroundColor = backgroundColor;
        }

        public DisplayedText(char value, ConsoleColor fontColor, ConsoleColor backgroundColor)
        :this(value.ToString(), fontColor, backgroundColor)
        {
        }

        public ConsoleColor FontColor { get; set; }
        
        public ConsoleColor BackgroundColor { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}

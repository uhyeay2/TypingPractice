namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    public class DisplayedString 
    {
        public string Value;

        public ConsoleColor FontColor;

        public ConsoleColor BackgroundColor;

        public DisplayedString(string value, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Value = value;
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        public DisplayedString(char value, ConsoleColor fontColor, ConsoleColor backgroundColor) 
            : this(value, 1, fontColor, backgroundColor) { }

        public DisplayedString(char value, int width, ConsoleColor fontColor, ConsoleColor backgroundColor) 
            : this(new string(value, width), fontColor, backgroundColor) { }

        public void Write()
        {
            Console.ForegroundColor = FontColor;
            Console.BackgroundColor = BackgroundColor;
            Console.Write(Value);
        }
    }
}

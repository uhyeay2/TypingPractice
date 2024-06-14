using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens
{
    public abstract class Screen
    {
        protected static ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        protected static ConsoleColor PrimaryFontColor { get; set; } = ConsoleColor.Green;

        protected static ConsoleColor SecondaryFontColor { get; set; } = ConsoleColor.Cyan;

        protected static ConsoleColor PrimaryBorderColor { get; set; } = ConsoleColor.Red;

        public static void Print(DisplayedSection content, bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            foreach (var line in content)
            {
                line.Write();
            }
        }

        public abstract Screen DisplayScreenAndGetNext();
    }
}

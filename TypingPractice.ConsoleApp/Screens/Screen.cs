using TypingPractice.ConsoleApp.Borders;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens
{
    public abstract class Screen
    {
        protected static readonly ConsoleWriter _consoleWriter = new();

        protected static readonly ScreenContentBuilder _contentBuilder = new();

        protected const ConsoleColor BorderColor = ConsoleColor.Red;

        protected const ConsoleColor BackgroundColor = ConsoleColor.Black;

        protected const ConsoleColor FontColor = ConsoleColor.Green;

        protected static readonly BaseBorder _defaultBorder = new StandardBorder(BorderColor, BackgroundColor);


        public abstract Screen DisplayScreenAndGetNext();
    }
}

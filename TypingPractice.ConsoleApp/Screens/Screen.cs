using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens
{
    public abstract class Screen
    {
        protected static readonly ConsoleWriter _consoleWriter = new();

        protected static readonly ScreenContentBuilder _contentBuilder = new();
        
        public abstract Screen DisplayScreenAndGetNext();
    }
}

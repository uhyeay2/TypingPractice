using TypingPractice.ConsoleApp.Screens;

namespace TypingPractice.ConsoleApp.Exceptions
{
    public class ConsoleSizeTooSmallException : Exception
    {
        private readonly Screen _screen;

        public ConsoleSizeTooSmallException(Screen screen) => _screen = screen;

        public Screen Screen => _screen;
    }
}

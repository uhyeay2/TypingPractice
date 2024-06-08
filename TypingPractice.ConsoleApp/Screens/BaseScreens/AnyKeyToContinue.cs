using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class AnyKeyToContinue : Screen
    {        
        public abstract ScreenContent GetScreenContent();

        public abstract Screen GetNextScreen();

        public override Screen DisplayScreenAndGetNext()
        {
            _consoleWriter.ClearThenWriteLines(GetScreenContent());

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

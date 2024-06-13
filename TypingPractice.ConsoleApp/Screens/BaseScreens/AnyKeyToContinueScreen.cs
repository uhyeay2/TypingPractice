using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class AnyKeyToContinueScreen : Screen
    {
        public abstract DisplayedContent GetScreenContent();

        public abstract Screen GetNextScreen();

        public override Screen DisplayScreenAndGetNext()
        {
            GetScreenContent().ClearAndPrint();

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

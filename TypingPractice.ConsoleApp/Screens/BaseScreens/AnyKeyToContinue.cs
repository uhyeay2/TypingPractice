using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class AnyKeyToContinue : Screen
    {        
        public abstract DisplayedSection GetDisplay();

        public abstract Screen GetNextScreen();

        public override Screen DisplayScreenAndGetNext()
        {
            Print(GetDisplay(), clearScreen: true);

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class AnyKeyToContinue : Screen
    {        
        public abstract DisplayedScreen GetDisplayedScreen();

        public abstract Screen GetNextScreen();

        public override Screen DisplayScreenAndGetNext()
        {
            GetDisplayedScreen().Write();

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

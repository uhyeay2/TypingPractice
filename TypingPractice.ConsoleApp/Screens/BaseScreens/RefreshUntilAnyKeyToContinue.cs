namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class RefreshUntilAnyKeyToContinue : RefreshableScreen
    {
        public abstract Screen GetNextScreen();

        public override Screen DisplayScreenAndGetNext()
        {
            Print(GetDisplay(), clearScreen: true);

            RefreshUntilKeyAvailable();

            Console.ReadKey(true);

            return GetNextScreen();
        }
    }
}

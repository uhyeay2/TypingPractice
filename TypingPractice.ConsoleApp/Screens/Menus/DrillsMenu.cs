using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Menus
{
    public class DrillsMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Typing Drills";

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Key Drills", new KeyFlashCardsMainMenu()),
            ("Previous", new MainMenu()),
            ("Quit", new ClosingScreen())
        ];
    }
}

using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Menus
{
    public class DrillsMenu : ScrollingMenu
    {
        protected override string MenuTitle => "Typing Drills";

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Key FlashCards", new KeyFlashCardsMainMenu()),
            ("Previous", new MainMenu()),
            ("Quit", new ClosingScreen())
        ];
    }
}

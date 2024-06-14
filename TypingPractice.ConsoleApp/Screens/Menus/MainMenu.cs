using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Menus
{
    public class MainMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Main Menu";

        public override int GetOptionsBorderWidth() => 47;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            //("Lessons", new LessonsMenu()),
            //("Adventure", new AdventureMenu()),
            ("Drills", new DrillsMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

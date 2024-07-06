using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Menus
{
    public class MainMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Main Menu";

        public override (string OptionMessage, Screen NextScreen)[] NextScreenOptions =>
        [
            //("Lessons", new LessonsMenu()),
            //("Adventure", new AdventureMenu()),
            ("Key Flash Cards", new KeyFlashCardsModeSelect()),
            ("Drills", new DrillsMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

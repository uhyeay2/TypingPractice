using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    public class KeyFlashCardsMainMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Key Flash Cards";

        public override int GetOptionsBorderWidth() => 100;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Practice Letters", new KeyFlashCardsLetterMenu()),
            ("Practice Num Row", new KeyFlashCardsNumberRowMenu()),
            ("Practice Fingers", new KeyFlashCardsSelectFingerMenu()),
            ("Practice All Keys", new KeyFlashCardsExerciseScreen("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%^&*();:'\",.-=_+[]{}/?1234567890")),
            ("Previous", new DrillsMenu()),
            ("Main Menu", new MainMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

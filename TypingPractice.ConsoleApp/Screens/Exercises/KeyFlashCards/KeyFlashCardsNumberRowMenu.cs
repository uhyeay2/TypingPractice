using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsNumberRowMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Num Row Flash Cards";

        public override int GetOptionsBorderWidth() => 110;

        public override (string OptionMessage, Screen NextScreen)[] NextScreenOptions =>
        [
            ("Only Numbers", new KeyFlashCardsExerciseScreen("1234567890")),
            ("Special Char", new KeyFlashCardsExerciseScreen("!@#$%^&*();:'\",.")),
            ("Special Char Adv.", new KeyFlashCardsExerciseScreen("!@#$%^&*();:'\",.-=_+[]{}/?")),
            ("Special / Num", new KeyFlashCardsExerciseScreen("!@#$%^&*();:'\",.?1234567890")),
            ("Special / Num Adv.", new KeyFlashCardsExerciseScreen("!@#$%^&*();:'\",.-=_+[]{}/?1234567890")),
            ("Previous", new KeyFlashCardsMainMenu()),
            ("Main Menu", new MainMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

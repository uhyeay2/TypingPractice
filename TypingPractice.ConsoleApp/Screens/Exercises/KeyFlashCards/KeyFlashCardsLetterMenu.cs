using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    internal class KeyFlashCardsLetterMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Letters Flash Cards";

        public override int GetOptionsBorderWidth() => 110;

        public override (string OptionMessage, Screen NextScreen)[] NextScreenOptions =>
        [
            ("Home Letters", new KeyFlashCardsExerciseScreen("asdfghjkl")),
            ("Home Letters Adv.", new KeyFlashCardsExerciseScreen("asdfghjklASDFGHJKL")),
            ("Top Letters", new KeyFlashCardsExerciseScreen("qwertyuiop")),
            ("Top Letters Adv.", new KeyFlashCardsExerciseScreen("qwertyuiopQWERTYUIOP")),
            ("Bottom Letters", new KeyFlashCardsExerciseScreen("zxcvbnm")),
            ("Bottom Letters Adv.", new KeyFlashCardsExerciseScreen("zxcvbnmZXCVBNM")),
            ("Lowercase Letters", new KeyFlashCardsExerciseScreen("qwertyuiopasdfghjklzxcvbnm")),
            ("Uppercase Letters", new KeyFlashCardsExerciseScreen("QWERTYUIOPASDFGHJKLZXCVBNM")),
            ("All Letters", new KeyFlashCardsExerciseScreen("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM")),
            ("Previous", new KeyFlashCardsMainMenu()),
            ("Main Menu", new MainMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

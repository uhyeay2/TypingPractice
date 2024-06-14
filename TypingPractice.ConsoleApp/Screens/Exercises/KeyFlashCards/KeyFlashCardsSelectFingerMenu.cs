using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    public class KeyFlashCardsSelectFingerMenu : DefaultScrollingMenu
    {
        public override string MenuTitle => "Finger Flash Cards";

        public override int GetOptionsBorderWidth() => 110;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Left Pinky", new KeyFlashCardsExerciseScreen("1!qQaAzZ")),
            ("Left Ring", new KeyFlashCardsExerciseScreen("2@wWsSxX")),
            ("Left Middle", new KeyFlashCardsExerciseScreen("3#eEdDcC")),
            ("Left Index", new KeyFlashCardsExerciseScreen("4$5%rRtTfFgGvVbB")),
            ("Right Pinky", new KeyFlashCardsExerciseScreen("0)pP;:/?")),
            ("Right Pinky Adv.", new KeyFlashCardsExerciseScreen("0)pP;:/?'\"[]{}-=_+")),
            ("Right Ring", new KeyFlashCardsExerciseScreen("9(oOlL.>")),
            ("Right Middle", new KeyFlashCardsExerciseScreen("8*iIkK,<")),
            ("Right Index", new KeyFlashCardsExerciseScreen("7&6^yYuUjJhHnNmM")),
            ("Previous", new KeyFlashCardsMainMenu()),
            ("Main Menu", new MainMenu()),
            ("Quit", new ClosingScreen()),
        ];
    }
}

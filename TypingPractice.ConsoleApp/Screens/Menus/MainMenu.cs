using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.ExpectedInputGenerators;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards;
using TypingPractice.ConsoleApp.Screens.Exercises.ScrollingWords;
using TypingPractice.ConsoleApp.Screens.Settings;
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
            ("Custom Settings", new GetExerciseSettingsScreen(_ => new KeyFlashCardsExercise(new KeyInputGeneratorRandomPrioritizeMistakes("asdfghjkl;qwertyuiopzxcvbnm,.1234567890!@#$%^&*()-_/<>?:'\""), _))),
            ("Word Scrolling", new ScrollingWordsExercise(new WordInputGeneratorRandom("1234567890!@#$%^&*()-_1234567890!@#$%^&*()-_asdfghjkl;'.,", generatedInputMinLength: 2, generatedInputMaxLength: 3), new ExerciseSettingsBuilder().Build())),
            ("Scrolling F", new ScrollingWordsExercise(new WordInputGeneratorOrdered("f", "fr", "ft", "fg", "fb", "fv"), new ExerciseSettingsBuilder().Build())),
            ("Custom Scroll", new GetExerciseSettingsScreen(_ => new ScrollingWordsExercise(new WordInputGeneratorRandomOrder(countOfRecentInputsToNotRepeat: 5,
                "for", "if", "when", "how", "why", "what", "you", "just", "hey", "where"
                ), _))),
            ("Key Flash Cards", new KeyFlashCardsModeSelect()),
            ("Quit", new ClosingScreen()),
        ];
    }
}
 
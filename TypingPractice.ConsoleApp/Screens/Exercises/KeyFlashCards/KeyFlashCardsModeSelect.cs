using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.KeyFlashCards
{
    public class KeyFlashCardsModeSelect : DefaultScrollingMenu
    {
        public override string MenuTitle => "Key Flash Cards";

        //private readonly string _pool = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%^&*();:'\",.-=_+[]{}/?1234567890<>";
        
        private readonly string _pool = "qwertyuiopasdfghjklzxcvbnm";

        public KeyFlashCardsModeSelect() { }

        public KeyFlashCardsModeSelect(string pool) => _pool = pool;

        public override (string OptionMessage, Screen NextScreen)[] NextScreenOptions => 
        [
            ("Hard Core", new KeyFlashCardsExercise(_pool, new ExerciseSettingsBuilder().EnableHardCoreMode().Build())),
            ("10 Words Per Minute", new KeyFlashCardsExercise(_pool, new ExerciseSettingsBuilder().SetMinimumWordsPerMinute(10).Build())),
            ("90 Seconds", new KeyFlashCardsExercise(_pool, new ExerciseSettingsBuilder().SetExerciseTimeLimit(90).Build())),
        ];
    }
}

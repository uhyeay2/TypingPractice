using TypingPractice.ConsoleApp.Exercises;
using TypingPractice.ConsoleApp.ExpectedInputGenerators;
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
            ("Hard Core", new KeyFlashCardsExercise(new KeyInputGeneratorOrdered(_pool), new ExerciseSettingsBuilder().SetLives(1, 1).Build())),
            ("2 Words Per Minute", new KeyFlashCardsExercise(new KeyInputGeneratorRandom(_pool), new ExerciseSettingsBuilder().SetMinimumWordsPerMinute(2).Build())),
            ("90 Seconds", new KeyFlashCardsExercise(new KeyInputGeneratorRandomPrioritizeMistakes(_pool, prioritizationMultiplier: 2), new ExerciseSettingsBuilder().SetExerciseTimeLimit(90).Build())),
        ];
    }
}

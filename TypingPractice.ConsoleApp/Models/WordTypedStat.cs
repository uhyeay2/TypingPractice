using System.Collections.ObjectModel;

namespace TypingPractice.ConsoleApp.Models
{
    public class WordTypedStat
    {
        private readonly string _expectedWord;

        private readonly List<KeyTypedStat> _keysTyped = [];

        public WordTypedStat(string expectedWord)
        {
            _expectedWord = expectedWord;
        }

        public ReadOnlyCollection<KeyTypedStat> KeysTyped => _keysTyped.AsReadOnly();

        public string ExpectedWord => _expectedWord;

        public double Milliseconds => _keysTyped.Sum(_ => _.Milliseconds);

        public int CountOfCorrectlyTypedKeys => _keysTyped.Count(_ => _.IsCorrectKeyTyped);

        public bool IsWordCompleted => CountOfCorrectlyTypedKeys == ExpectedWord.Length;

        public bool IsAnyMistakesMade => _keysTyped.Count > 0 && _keysTyped.Any(_ => !_.IsCorrectKeyTyped);

        public void AddKeyTyped(KeyTypedStat keyTypedStat) => _keysTyped.Add(keyTypedStat);
    }
}

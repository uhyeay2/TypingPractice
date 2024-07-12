using System.Text;

namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public class WordInputGeneratorRandom : ExpectedInputGenerator<string>
    {
        private readonly string _pool;

        private List<string> _recentInputsGenerated = [];

        private readonly int _countOfRecentInputsToNotRepeat;

        private readonly int _generatedInputMinLength;

        private readonly int _generatedInputMaxLength;

        private readonly Random _rng = new();

        public WordInputGeneratorRandom(string pool, int countOfRecentInputsToNotRepeat = 5, int generatedInputMinLength = 1, int generatedInputMaxLength = 4)
        {
            _pool = pool;

            var distinctCount = _pool.Distinct().Count();

            while (countOfRecentInputsToNotRepeat > distinctCount)
            {
                countOfRecentInputsToNotRepeat--;
            }

            _countOfRecentInputsToNotRepeat = countOfRecentInputsToNotRepeat;

            _generatedInputMinLength = generatedInputMinLength;

            _generatedInputMaxLength = generatedInputMaxLength;
        }

        public override string GetNextExpectedInput()
        {
            string input;

            do
            {
                var inputLength = _rng.Next(_generatedInputMinLength, _generatedInputMaxLength + 1);

                var sb = new StringBuilder();

                while (sb.Length < inputLength)
                {
                    var randomCharacter = _pool[_rng.Next(_pool.Length)];

                    sb.Append(randomCharacter);
                }

                input = sb.ToString();

            } while (_recentInputsGenerated.Contains(input));

            _recentInputsGenerated.Add(input);

            if (_recentInputsGenerated.Count > _countOfRecentInputsToNotRepeat)
            {
                _recentInputsGenerated.RemoveAt(0);
            }

            return input;
        }
    }
}
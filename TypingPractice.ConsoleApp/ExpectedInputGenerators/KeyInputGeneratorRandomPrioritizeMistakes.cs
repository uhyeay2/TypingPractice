using System.Text;

namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public class KeyInputGeneratorRandomPrioritizeMistakes : KeyInputGeneratorRandom
    {
        private readonly int _countOfRecentErrorsToPrioritize;

        private readonly int _prioritizationMultiplier;

        public KeyInputGeneratorRandomPrioritizeMistakes(string pool, int countOfRecentInputsToNotRepeat = 5, int countOfRecentErrorsToPrioritize = 5, int prioritizationMultiplier = 2) : base(pool, countOfRecentInputsToNotRepeat)
        {
            _countOfRecentErrorsToPrioritize = countOfRecentErrorsToPrioritize;

            _prioritizationMultiplier = prioritizationMultiplier <= 0? 1 : prioritizationMultiplier;
        }

        public override char GetNextExpectedInput()
        {
            if (_mistakes.Count == 0 || _countOfRecentErrorsToPrioritize <= 0)
            {
                return base.GetNextExpectedInput();
            }

            List<char> prioritizedMistakes = [];

            var mistakeIndex = _mistakes.Count - 1;

            while (prioritizedMistakes.Count < _countOfRecentErrorsToPrioritize && mistakeIndex >= 0)
            {
                var mistake = _mistakes.ElementAt(mistakeIndex);

                if (!prioritizedMistakes.Contains(mistake) && !_recentInputsGenerated.Contains(mistake))
                {
                    prioritizedMistakes.Add(mistake);
                }

                mistakeIndex--;
            }
            
            var mistakesPool = new string(prioritizedMistakes.ToArray());
            
            var sb = new StringBuilder();

            // Add a random input the same number of times as the number of mistakes we are prioritizing.   
            for (int i = 0; i <= prioritizedMistakes.Count; i++)
            {
                sb.Append(GetRandomCharFromPoolNotRecentlyGenerated());
            }

            // Add the recent mistakes however many times we are multiplying their prioritization
            for (int i = 0; i < _prioritizationMultiplier; i++)
            {
                sb.Append(mistakesPool);
            }

            var generatedInput = sb[_rng.Next(sb.Length)];

            _recentInputsGenerated.Add(generatedInput);

            if (_recentInputsGenerated.Count > _countOfRecentInputsToNotRepeat)
            {
                _recentInputsGenerated.RemoveAt(0);
            }

            return generatedInput;
        }
    }
}

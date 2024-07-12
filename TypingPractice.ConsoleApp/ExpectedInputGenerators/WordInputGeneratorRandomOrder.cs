namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    internal class WordInputGeneratorRandomOrder : ExpectedInputGenerator<string>
    {
        private readonly string[] _pool;

        private List<string> _recentInputsGenerated = [];

        private readonly int _countOfRecentInputsToNotRepeat;

        private readonly Random _rng = new();

        public WordInputGeneratorRandomOrder(int countOfRecentInputsToNotRepeat = 5, params string[] pool)
        {
            _pool = pool;

            var distinctCount = _pool.Distinct().Count();

            while (countOfRecentInputsToNotRepeat > distinctCount)
            {
                countOfRecentInputsToNotRepeat--;
            }

            _countOfRecentInputsToNotRepeat = countOfRecentInputsToNotRepeat;
        }

        public override string GetNextExpectedInput()
        {
            string input;

            do
            {
                input = _pool[_rng.Next(_pool.Length)];

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

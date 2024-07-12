namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public class KeyInputGeneratorRandom : ExpectedInputGenerator<char>
    {
        protected readonly string _pool;

        protected List<char> _recentInputsGenerated = [];

        protected readonly int _countOfRecentInputsToNotRepeat;

        protected readonly Random _rng = new();

        public KeyInputGeneratorRandom(string pool, int countOfRecentInputsToNotRepeat = 5)
        {
            _pool = pool;

            var distinctCount = _pool.Distinct().Count();

            while (countOfRecentInputsToNotRepeat > distinctCount)
            {
                countOfRecentInputsToNotRepeat--;
            }

            _countOfRecentInputsToNotRepeat = countOfRecentInputsToNotRepeat;
        }

        protected char GetRandomCharFromPoolNotRecentlyGenerated()
        {
            char randomChar;

            do
            {
                randomChar = _pool[_rng.Next(_pool.Length)];

            } while (_recentInputsGenerated.Contains(randomChar));

            return randomChar;
        }

        public override char GetNextExpectedInput()
        {
            var randomChar = GetRandomCharFromPoolNotRecentlyGenerated();

            _recentInputsGenerated.Add(randomChar);

            if (_recentInputsGenerated.Count > _countOfRecentInputsToNotRepeat)
            {
                _recentInputsGenerated.RemoveAt(0);
            }

            return randomChar;
        }
    }
}

namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public class WordInputGeneratorOrdered : ExpectedInputGenerator<string>
    {
        private readonly string[] _pool;

        private int _currentIndex = 0;

        public WordInputGeneratorOrdered(params string[] pool) => _pool = pool;

        public override string GetNextExpectedInput()
        {
            if (_currentIndex >= _pool.Length)
            {
                _currentIndex = 0;
            }

            return _pool[_currentIndex++];
        }
    }
}

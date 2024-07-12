namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public class KeyInputGeneratorOrdered : ExpectedInputGenerator<char>
    {
        private readonly string _pool;

        private int _currentIndex = 0;

        public KeyInputGeneratorOrdered(string pool) => _pool = pool;
        
        public override char GetNextExpectedInput()
        {
            if (_currentIndex >= _pool.Length)
            {
                _currentIndex = 0;
            }

            return _pool[_currentIndex++];
        }
    }
}

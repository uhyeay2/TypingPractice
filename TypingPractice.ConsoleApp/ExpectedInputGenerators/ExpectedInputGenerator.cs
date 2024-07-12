namespace TypingPractice.ConsoleApp.ExpectedInputGenerators
{
    public abstract class ExpectedInputGenerator<T>
    {
        protected readonly List<T> _mistakes = [];

        public abstract T GetNextExpectedInput();

        public void NotifyOfMistake(T mistake)
        {
            _mistakes.Add(mistake);
        }
    }
}

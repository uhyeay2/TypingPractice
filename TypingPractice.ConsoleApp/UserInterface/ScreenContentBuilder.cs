namespace TypingPractice.ConsoleApp.UserInterface
{
    public class ScreenContentBuilder
    {
        public ScreenContent New(Func<ScreenContentToBeBuilt, ScreenContentToBeBuilt> func) => func.Invoke(new()).Build();
    }
}

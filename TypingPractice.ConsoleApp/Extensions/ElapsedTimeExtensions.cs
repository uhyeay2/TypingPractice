namespace TypingPractice.ConsoleApp.Extensions
{
    public static class ElapsedTimeExtensions
    {
        public static string TimeStamp(this TimeSpan timespan) =>
            $"{timespan:hh}:{timespan:mm}:{timespan:ss}.{timespan:ff}";
    }
}

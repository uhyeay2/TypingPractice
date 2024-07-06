namespace TypingPractice.ConsoleApp.Constants
{
    public static class ConsoleSize
    {
        public static bool IsMeetingMinimumRequirements => 
            Console.WindowHeight >= ConsoleSize.MinimumHeight && Console.WindowWidth >= ConsoleSize.MinimumWidth;

        public const int MinimumWidth = 180;

        public const int MinimumHeight = 40;
    }
}

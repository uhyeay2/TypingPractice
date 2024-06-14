namespace TypingPractice.ConsoleApp.Extensions
{
    public static class ConsoleColorExtension
    {
        public static ConsoleColor CycleColor(this ConsoleColor color) =>
            color switch
            {
                ConsoleColor.Cyan => ConsoleColor.Blue,
                ConsoleColor.Blue => ConsoleColor.DarkBlue,
                ConsoleColor.DarkBlue => ConsoleColor.DarkGreen,
                ConsoleColor.DarkGreen => ConsoleColor.Green,
                ConsoleColor.Green => ConsoleColor.Yellow,
                ConsoleColor.Yellow => ConsoleColor.DarkYellow,
                ConsoleColor.DarkYellow => ConsoleColor.Red,
                ConsoleColor.Red => ConsoleColor.DarkRed,
                ConsoleColor.DarkRed => ConsoleColor.Magenta,
                ConsoleColor.DarkMagenta => ConsoleColor.DarkGray,
                ConsoleColor.DarkGray => ConsoleColor.Gray,
                ConsoleColor.Gray => ConsoleColor.White,
                _ => ConsoleColor.Cyan
            };
    }
}

namespace TypingPractice.ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Return the string provided with whitespace padded on both sides evenly to make the string match the targetLength provided.
        /// Throws exception if targetLength is less than str.Length
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string PadToCenter(this string str, int targetLength)
        {
            if (str.Length > targetLength)
            {
                throw new ArgumentOutOfRangeException($"The string ({str}) cannot be padded to the center of the targetLength. " +
                    $"- the targetLength: ({targetLength}) is shorter than the string currently is ({str} - Length: {str.Length})");
            }

            // formula for centering string to target length
            var leftPaddingSize = (targetLength + str.Length) / 2;

            return str.PadLeft(leftPaddingSize).PadRight(targetLength);
        }
    }
}

namespace TypingPractice.ConsoleApp.Extensions
{
    public static class CharacterExtensions
    {
        public static string ExpectedCharacterDescription(this char c)
        {

            if (char.IsWhiteSpace(c))
            {
                return "    Space   ";
            }

            if (char.IsUpper(c))
            {
                return $"UPPERCASE: {c}";
            }

            if (char.IsLower(c))
            {
                return $"lowercase: {c}";
            }            

            if (char.IsNumber(c))
            {
                return $"   Number: {c}";
            }

            return $"  Special: {c}";
        }
    }
}

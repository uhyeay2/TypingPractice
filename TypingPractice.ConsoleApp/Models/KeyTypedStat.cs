namespace TypingPractice.ConsoleApp.Models
{
    public class KeyTypedStat
    {
        public KeyTypedStat(KeyTypedStat? previousKeyTyped, double milliseconds, char characterTyped, char expectedCharacter)
        {
            PreviousKeyTyped = previousKeyTyped;
            Milliseconds = milliseconds;
            CharacterTyped = characterTyped;
            ExpectedCharacter = expectedCharacter;
        }

        public KeyTypedStat? PreviousKeyTyped { get; set; }

        public double Milliseconds { get; set; }

        public char CharacterTyped { get; set; }

        public char ExpectedCharacter { get; set; }

        public bool IsCorrectKeyTyped => CharacterTyped == ExpectedCharacter;
    }
}

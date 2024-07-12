using TypingPractice.ConsoleApp.ExpectedInputGenerators;

namespace TypingPractice.ConsoleApp.Tests.ExpectedInputGeneratorTests
{
    public class KeyInputGeneratorOrderedTests
    {
        [Fact]
        public void GetNextExpectedInput_Given_PoolContainsOneCharacter_ShouldReturn_SameCharacter()
        {
            string pool = "e";
            
            var numberOfInputsToGenerate = 5;

            var expectedOutput = "eeeee";

            var generator = new KeyInputGeneratorOrdered(pool);

            var results = GenerateInputs(generator, numberOfInputsToGenerate);

            Assert.Equal(expectedOutput, results);
        }

        [Theory]
        [InlineData("123", 3, "123")]
        [InlineData("123", 4, "1231")]
        [InlineData("123", 7, "1231231")]
        public void GetExpectedInput_Given_PoolContainsManyCharacters_ShouldReturn_CharactersInOrder(string pool, int numberOfInputsToGenerate, string expectedOutput)
        {
            var generator = new KeyInputGeneratorOrdered(pool);

            var results = GenerateInputs(generator, numberOfInputsToGenerate);

            Assert.Equal(expectedOutput, results);
        }

        /// <summary>
        /// Helper for generating inputs for tests
        /// </summary>
        private string GenerateInputs(KeyInputGeneratorOrdered generator, int numberOfInputsToGenerate)
        {
            char[] results = new char[numberOfInputsToGenerate];

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = generator.GetNextExpectedInput();
            }

            return new string(results);
        }
    }
}

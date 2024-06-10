using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Tests.ExtensionTests
{
    public class CharacterExtensionTests
    {
        [Theory]
        [InlineData('a', "lowercase: a")]
        [InlineData('b', "lowercase: b")]
        [InlineData('c', "lowercase: c")]
        [InlineData('A', "UPPERCASE: A")]
        [InlineData('B', "UPPERCASE: B")]
        [InlineData('C', "UPPERCASE: C")]
        [InlineData('1', "   Number: 1")]
        [InlineData('2', "   Number: 2")]
        [InlineData('3', "   Number: 3")]
        [InlineData('!', "  Special: !")]
        [InlineData('@', "  Special: @")]
        [InlineData('#', "  Special: #")]
        [InlineData('$', "  Special: $")]
        [InlineData('%', "  Special: %")]
        [InlineData('^', "  Special: ^")]
        [InlineData('&', "  Special: &")]
        [InlineData('*', "  Special: *")]
        [InlineData('(', "  Special: (")]
        [InlineData(')', "  Special: )")]
        [InlineData('-', "  Special: -")]
        [InlineData('=', "  Special: =")]
        [InlineData('_', "  Special: _")]
        [InlineData('+', "  Special: +")]
        [InlineData('[', "  Special: [")]
        [InlineData(']', "  Special: ]")]
        [InlineData('{', "  Special: {")]
        [InlineData('}', "  Special: }")]
        [InlineData(';', "  Special: ;")]
        [InlineData(':', "  Special: :")]
        [InlineData('<', "  Special: <")]
        [InlineData('>', "  Special: >")]
        [InlineData(',', "  Special: ,")]
        [InlineData('.', "  Special: .")]
        [InlineData('?', "  Special: ?")]
        [InlineData('/', "  Special: /")]
        [InlineData('\\', "  Special: \\")]
        [InlineData('\'', "  Special: \'")]
        [InlineData('"', "  Special: \"")]
        public void CharacterExtension_ToStringWithDescription_Given_Character_ShouldReturn_ExpectedString(char character, string expected)
        {
            var result = character.ExpectedCharacterDescription();

            Assert.Equal(expected, result);
        }
    }
}

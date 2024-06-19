using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    internal class FireCauldronAnimation : Animation
    {
        private ConsoleColor _cauldronColor = ConsoleColor.DarkGray;

        private ConsoleColor _backgroundColor = ConsoleColor.Black;

        private static readonly ConsoleColor[] PossibleFireColors =
        {
            ConsoleColor.Red,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkYellow,
        };

        private static ConsoleColor GetRandomFireColor() => PossibleFireColors[Random.Shared.Next(PossibleFireColors.Length)];

        private ConsoleColor GetCharacterColor(char c, int frameLine)
        {
            if (frameLine <= 1)
            {
                return c == '_' ? _cauldronColor : GetRandomFireColor();
            }
            else
            {
                return c == '_' || c == '/' || c == '\\' ? _cauldronColor : GetRandomFireColor();
            }
        }

        private static readonly string[][] _frames = [
            [
                @"   .`,' .' `,.   ",
                @"___,/(./(,`/(,___",
                @"\  \__)__)(__/  /",
                @" \__   ___   __/ ",
                @"    \_/   \_/    ",
            ],
            [
                @"   ,`,' ./(.`,  ",
                @"___)\,)\( ,)\.___",
                @"\  \_)__)_(__/  /",
                @" \__   ___   __/ ",
                @"    \_/   \_/    ",
            ],
            [
                @"   ,`)\.`, ' .`  ",
                @"___/(. )/(,`/(___",
                @"\  \_)_(__)(_/  /",
                @" \__   ___   __/ ",
                @"    \_/   \_/    ",
            ],
            [
                @"  '.` , ' .`,'.  ",
                @"___)\ )\,` )\.___",
                @"\  \_)__)_(__/  /",
                @" \__   ___   __/ ",
                @"    \_/   \_/    ",
            ],
        ];

        public override long RefreshRateInMilliseconds => 200;

        public override DisplayedSection AsDisplayedSection()
        {
            var displayedSection = new DisplayedSection();

            for (int i = 0; i < _frames[_currentFrame].Length; i++)
            {
                var displayedLine = new DisplayedLine(
                    _frames[_currentFrame][i].Select((_) => new DisplayedString(_, GetCharacterColor(_, i), _backgroundColor)));

                displayedSection.Add(displayedLine);
            }

            return displayedSection;
        }

        private int _currentFrame = 0;

        public override void ChangeFrame()
        {
            if (_currentFrame >= _frames.Length - 1)
            {
                _currentFrame = 0;
            }
            else
            {
                _currentFrame++;
            }
        }
    }
}

using Figgle;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    internal class ColorChangingArrowGoingLeft : Animation
    {
        #region Private Members

        private readonly ConsoleColor _backgroundColor;

        private readonly string[] _frames = [
            "    < ",
            "   <  ",
            "   <  ",
            "  <   ",
            "  <   ",
            " <    ",
            " <    ",
            " <    ",
            "  <   ",
            "  <   ",
            "   <  ",
            "   <  ",
            "    < ",
        ];

        private int _currentFrame = 0;

        private int colorCycleCount = 0;

        private ConsoleColor _fontColor;

        #endregion

        #region Constructor

        public ColorChangingArrowGoingLeft(ConsoleColor backgroundColor) => _backgroundColor = backgroundColor;

        #endregion

        #region Animation Overrides

        public override long RefreshRateInMilliseconds => 35;

        public override void ChangeAnimation()
        {
            _fontColor = _fontColor.CycleColor();

            // Change frame every four times that color changes.
            if (colorCycleCount == 3)
            {
                if (_currentFrame >= _frames.Length - 1)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame++;
                }

                colorCycleCount = 0;
            }
            else
            {
                colorCycleCount++;
            }
        }

        public override DisplayedSection Display() => new(_fontColor, _backgroundColor, FiggleFonts.Doom, _frames[_currentFrame]);

        #endregion
    }
}

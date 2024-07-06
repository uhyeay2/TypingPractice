using Figgle;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    public class Signature : Animation
    {
        #region Private Members

        private ConsoleColor _fontColor;

        private ConsoleColor _backgroundColor;

        #endregion

        #region Constructor

        public Signature(ConsoleColor backgroundColor) => _backgroundColor = backgroundColor;

        #endregion

        #region Animation Overrieds

        public override long RefreshRateInMilliseconds => 125;

        public override void ChangeAnimation()
        {
            do
            {
                _fontColor = _fontColor.CycleColor();
            }
            while (_fontColor == _backgroundColor);
        }

        public override DisplayedSection Display() => new (_fontColor, _backgroundColor, FiggleFonts.CyberMedium, "By: Daniel Aguirre");
        
        #endregion
    }
}

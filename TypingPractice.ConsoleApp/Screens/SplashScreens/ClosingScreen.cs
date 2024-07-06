using Figgle;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class ClosingScreen : RefreshUntilAnyKeyToContinue
    {
        private readonly Signature _signature = new(BackgroundColor);

        public override IEnumerable<Animation> GetAnimations() => [_signature];

        private const int _borderWidth = 100;

        private const int _borderHeight = 28;

        public override DisplayedSection GetDisplay() =>
            new DisplayInsideSimpleBorder(PrimaryBorderColor, BackgroundColor, _borderWidth, _borderHeight,
                new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall,
                    "Typing  Practice"),
                new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall,
                    "THANK YOU",
                    "FOR PLAYING",
                    "TYPING PRACTICE",
                    "COME BACK SOON!",
                    ""),
                _signature.Display()
            ).Centered(BackgroundColor);

        public override Screen GetNextScreen() => this;
    }
}

using Figgle;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : RefreshUntilAnyKeyToContinue
    {
        private readonly Signature _signature = new(BackgroundColor);

        public override IEnumerable<Animation> GetAnimations() => [_signature];

        private const int _borderWidth = 86;

        private const int _borderHeight = 27;

        public override DisplayedSection GetDisplay() =>
            new DisplayInsideSimpleBorder(PrimaryBorderColor, BackgroundColor, _borderWidth, _borderHeight,
                new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall,
                    "Typing  Practice"),
                new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall,
                    "~1234567890-= ",
                    " QWERTYUIOP[]\\",
                    " ASDFGHJKL;' ",
                    " ZXCVBNM,./ "),
                new DisplayedSection(SecondaryFontColor, BackgroundColor,
                    "",
                    "Press Any Key To Continue."),
                _signature.Display()
            ).Centered(BackgroundColor);

        public override Screen GetNextScreen() => new MainMenu();
    }
}

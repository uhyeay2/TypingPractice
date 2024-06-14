using Figgle;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class ClosingScreen : RefreshUntilAnyKeyToContinue
    {
        private const int _borderWidth = 100;

        private const int _borderHeight = 28;

        private ConsoleColor _signatureColor;

        public override long RefreshRatioInMilliseconds => 60;

        public override void TriggerRefresh()
        {
            base.TriggerRefresh();

            _signatureColor = _signatureColor.CycleColor();
        }

        public override DisplayedSection GetDisplay() => new(
            new BasicBorder(PrimaryBorderColor, BackgroundColor, _borderWidth, _borderHeight, 
                new DisplayedSection(
                    new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Typing  Practice"),
                    new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall, "THANK YOU",
                                                                                                         "FOR PLAYING",
                                                                                                         "TYPING PRACTICE",
                                                                                                         "COME BACK SOON!"),
                    new DisplayedSection(_signatureColor, BackgroundColor, FiggleFonts.CyberMedium, "", "By: Daniel Aguirre")
                )
            ).ToDisplayedSection()
            .Centered(BackgroundColor)
        );

        public override Screen GetNextScreen() => this;
    }
}

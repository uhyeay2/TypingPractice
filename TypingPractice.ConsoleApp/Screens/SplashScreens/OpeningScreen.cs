using Figgle;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : RefreshUntilAnyKeyToContinue
    {        
        private ConsoleColor _signatureColor;

        private const int _borderWidth = 86;

        private const int _borderHeight = 27;

        private static readonly DisplayedSection Header = 
            new(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Typing  Practice");

        private static readonly DisplayedSection KeyboardSection =
            new(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall, "~1234567890-= ",
                                                                             " QWERTYUIOP[]\\",
                                                                             " ASDFGHJKL;' ",
                                                                             " ZXCVBNM,./ ");
        private DisplayedSection Signature =>
            new(_signatureColor, BackgroundColor, FiggleFonts.CyberMedium, "By: Daniel Aguirre");

        private static DisplayedSection PromptToClickKey =
            new(SecondaryFontColor, BackgroundColor, "", "Press Any Key To Continue.");

        public override long RefreshRatioInMilliseconds => 60;

        public override void TriggerRefresh()
        {
            base.TriggerRefresh();

            _signatureColor = _signatureColor.CycleColor();
        }

        public override DisplayedSection GetDisplay() => new BasicBorder(PrimaryBorderColor, BackgroundColor, _borderWidth, _borderHeight,
                Header, KeyboardSection, Signature, PromptToClickKey).ToDisplayedSection().Centered(BackgroundColor);

        public override Screen GetNextScreen() => new MainMenu();
    }
}

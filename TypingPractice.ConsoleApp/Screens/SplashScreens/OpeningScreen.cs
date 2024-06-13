using Figgle;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : RefreshUntilAnyKeyToContinue
    {        
        private const ConsoleColor _borderColor = ConsoleColor.Red;

        private const ConsoleColor _backgroundColor = ConsoleColor.Black;

        private ConsoleColor _signatureColor;

        private const int _borderWidth = 86;

        private const int _borderHeight = 27;

        private static readonly DisplayedSection Header = 
            new(ConsoleColor.Green, _backgroundColor, FiggleFonts.SlantSmall, "Typing  Practice");

        private static readonly DisplayedSection KeyboardSection =
            new(ConsoleColor.Cyan, _backgroundColor, FiggleFonts.KeyboardSmall, "~1234567890-= ",
                                                                             " QWERTYUIOP[]\\",
                                                                             " ASDFGHJKL;' ",
                                                                             " ZXCVBNM,./ ");
        private DisplayedSection Signature =>
            new(_signatureColor, _backgroundColor, FiggleFonts.CyberMedium, "By: Daniel Aguirre");

        private static DisplayedSection PromptToClickKey =
            new(ConsoleColor.Cyan, _backgroundColor,"", "Press Any Key To Continue.");

        public override long RefreshRatioInMilliseconds => 60;

        public override void TriggerRefresh()
        {
            base.TriggerRefresh();

            _signatureColor = (_signatureColor) switch
            {
                ConsoleColor.Cyan => ConsoleColor.Blue,
                ConsoleColor.Blue => ConsoleColor.DarkBlue,
                ConsoleColor.DarkBlue => ConsoleColor.DarkGreen,
                ConsoleColor.DarkGreen => ConsoleColor.Green,
                ConsoleColor.Green => ConsoleColor.Yellow,
                ConsoleColor.Yellow => ConsoleColor.DarkYellow,
                ConsoleColor.DarkYellow => ConsoleColor.Red,
                ConsoleColor.Red => ConsoleColor.DarkRed,
                ConsoleColor.DarkRed => ConsoleColor.Magenta,
                ConsoleColor.DarkMagenta => ConsoleColor.DarkGray,
                ConsoleColor.DarkGray => ConsoleColor.Gray,
                ConsoleColor.Gray => ConsoleColor.White,
                _ => ConsoleColor.Cyan
            };
        }

        public override DisplayedSection GetDisplay() => 
            new BasicBorder(_borderColor, _backgroundColor, _borderWidth, _borderHeight,
                Header, KeyboardSection, Signature, PromptToClickKey
            )
            .ToDisplayedSection()
            .Centered(_backgroundColor);

        public override Screen GetNextScreen() => new MainMenu();
    }
}

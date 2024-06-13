using Figgle;
using TypingPractice.ConsoleApp.Borders;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : AnyKeyToContinueScreen
    {        
        private const ConsoleColor KeyboardColor = ConsoleColor.Cyan;

        private static readonly BaseBorder _border = new StandardBorder(BorderColor, BackgroundColor);

        public override DisplayedContent GetScreenContent() => new(
            _border.Apply(
                FiggleFonts.SlantSmall.RenderIEnumerable("Typing  Practice").AsDisplayedLine(FontColor, BackgroundColor),
                FiggleFonts.KeyboardSmall.RenderIEnumerable("~1234567890-= ",
                                                            " QWERTYUIOP[]\\",
                                                            " ASDFGHJKL;' ",
                                                            " ZXCVBNM,./ ").AsDisplayedLine(KeyboardColor, BackgroundColor),
                FiggleFonts.Italic.RenderIEnumerable("By:  Daniel  Aguirre").AsDisplayedLine(FontColor, BackgroundColor)
                .Append(new("Press any key to continue.", FontColor, BackgroundColor))
            )
            .PadToCenterOfConsole(BackgroundColor)
        );

        public override Screen GetNextScreen() => new MainMenu();
    }
}

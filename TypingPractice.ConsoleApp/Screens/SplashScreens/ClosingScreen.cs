using Figgle;
using TypingPractice.ConsoleApp.Borders;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class ClosingScreen : AnyKeyToContinueScreen
    {
        private const ConsoleColor KeyboardColor = ConsoleColor.Cyan;

        private static readonly BaseBorder _border = new StandardBorder(BorderColor, BackgroundColor);

        public override DisplayedContent GetScreenContent() => new(
             _border.Apply(
                FiggleFonts.SlantSmall.RenderIEnumerable("Typing  Practice").AsDisplayedLine(FontColor, BackgroundColor),
                FiggleFonts.KeyboardSmall.RenderIEnumerable("THANK YOU",
                                                            "FOR PLAYING",
                                                            "TYPING PRACTICE",
                                                            "COME BACK SOON!").AsDisplayedLine(KeyboardColor, BackgroundColor),
                FiggleFonts.Italic.RenderIEnumerable("By:  Daniel  Aguirre").AsDisplayedLine(FontColor, BackgroundColor)
            )
            .PadToCenterOfConsole(BackgroundColor)
        );

        public override Screen GetNextScreen() => this;
    }
}

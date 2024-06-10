using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Menus;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : AnyKeyToContinue
    {
        public override ScreenContent GetScreenContent() => _contentBuilder.New(_ => _.AddContent(
            Center.HorizontalByConsoleWidth(Border.Apply(
                FiggleFonts.SlantSmall.RenderIEnumerable("Typing  Practice"),
                FiggleFonts.KeyboardSmall.RenderIEnumerable("~1234567890-= ", 
                                                            " QWERTYUIOP[]\\",
                                                            " ASDFGHJKL;' ",
                                                            " ZXCVBNM,./ "),
                FiggleFonts.Italic.RenderIEnumerable("By:  Daniel  Aguirre")
                .Append("Press any key to continue."))
            )));

        public override Screen GetNextScreen() => new MainMenu();
    }
}

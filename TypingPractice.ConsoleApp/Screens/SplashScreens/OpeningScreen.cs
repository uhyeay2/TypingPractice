using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.Screens.Exercises;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class OpeningScreen : AnyKeyToContinue
    {

        public override ScreenContent GetScreenContent() => _contentBuilder.New(_ =>
            _.AddContent(
                FiggleFonts.SlantSmall.RenderIEnumerable(
                    "Typing  Practice"
                ),
                FiggleFonts.KeyboardSmall.RenderIEnumerable(
                    "~1234567890-= ", 
                    " QWERTYUIOP[]\\",
                    " ZXCVBNM,./ ",
                    " ASDFGHJKL;' "
                ),
                //FiggleFonts.KeyboardSmall.RenderIEnumerable("~1234567890-= "),
                //FiggleFonts.KeyboardSmall.RenderIEnumerable(" QWERTYUIOP[]\\"),
                //FiggleFonts.KeyboardSmall.RenderIEnumerable(" ASDFGHJKL;' "),
                //FiggleFonts.KeyboardSmall.RenderIEnumerable(" ZXCVBNM,./ "),
                FiggleFonts.Italic.RenderIEnumerable("By:  Daniel  Aguirre"),
                ["Press any key to continue."]
            )
            .ApplyBorder()            
            .CenterByConsoleSize(vertical: false)
            );

        public override Screen GetNextScreen() => new KeyPractice();
    }
}

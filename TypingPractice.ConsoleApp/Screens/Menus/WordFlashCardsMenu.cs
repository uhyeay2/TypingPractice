using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.Menus
{
    public class WordFlashCardsMenu : AnyKeyToContinue
    {
        public override Screen GetNextScreen() => new MainMenu();

        public override ScreenContent GetScreenContent() => _contentBuilder.New(_ =>
            _.AddContent(
                Center.ByConsoleWidthAndHeight(
                    Border.Apply(
                        FiggleFonts.SlantSmall.RenderIEnumerable("Sorry!",
                                                                 "Word Flash Cards",
                                                                 "are not available yet.",
                                                                 "Try again later!")
                    )
                )
            )
        );
    }
}

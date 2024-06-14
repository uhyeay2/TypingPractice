using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class PauseScreen : DefaultScrollingMenu
    {
        private readonly Screen _screenToResumeTo, _screenToQuitTo;

        private readonly DisplayedSection _contentAboveMenu;

        public PauseScreen(Screen screenToReturnTo, Screen menuToQuitTo, DisplayedSection? contentAboveMenu = null)
        {
            _screenToResumeTo = screenToReturnTo;

            _screenToQuitTo = menuToQuitTo;

            _contentAboveMenu = contentAboveMenu ?? [];
        }

        public override string MenuTitle => "Pause Menu";

        public override DisplayedSection Header() => new (
            base.Header(), 
            _contentAboveMenu
        );

        public override int GetOptionsBorderWidth() => 60;

        public override int GetOptionsBorderHeight() => 10;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Resume", _screenToResumeTo),
            ("Quit", _screenToQuitTo),
        ];
    }
}

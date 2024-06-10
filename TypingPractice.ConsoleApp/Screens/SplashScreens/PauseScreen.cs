using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class PauseScreen : ScrollingMenu
    {
        private readonly Screen _screenToResumeTo, _screenToQuitTo;

        public PauseScreen(Screen screenToReturnTo, Screen menuToQuitTo)
        {
            _screenToResumeTo = screenToReturnTo;

            _screenToQuitTo = menuToQuitTo;
        }

        protected override string MenuTitle => "Pause Menu";

        public override int GetOptionsBorderHeight() => 10;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Resume", _screenToResumeTo),
            ("Quit", _screenToQuitTo),
        ];
    }
}

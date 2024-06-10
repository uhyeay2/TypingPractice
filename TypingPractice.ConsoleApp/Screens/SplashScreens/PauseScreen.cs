using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class PauseScreen : ScrollingMenu
    {
        private readonly Screen _screenToResumeTo, _screenToQuitTo;

        private readonly IEnumerable<string> _contentAboveOptions;

        public PauseScreen(Screen screenToReturnTo, Screen menuToQuitTo, IEnumerable<string>? contentAboveOptions = null)
        {
            _screenToResumeTo = screenToReturnTo;

            _screenToQuitTo = menuToQuitTo;

            _contentAboveOptions = contentAboveOptions ?? [];
        }

        protected override string MenuTitle => "Pause Menu";

        public override IEnumerable<string> GenerateContentAboveMenu() =>
            FiggleFonts.SlantSmall.RenderIEnumerable("Pause").Concat(_contentAboveOptions);

        public override int GetOptionsBorderWidth() => 60;

        public override int GetOptionsBorderHeight() => 10;

        public override IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions() =>
        [
            ("Resume", _screenToResumeTo),
            ("Quit", _screenToQuitTo),
        ];
    }
}

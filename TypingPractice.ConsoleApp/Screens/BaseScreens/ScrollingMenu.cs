using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ScrollingMenu : Screen
    {
        #region Abstract Method For Defining Options/Screens For The Menu

        protected abstract string MenuTitle { get; }

        public abstract IEnumerable<(string OptionMessage, Screen NextScreen)> GetOptions();

        #endregion

        #region Overrideable Methods For Defining Screen Content Above/Below Menu

        public virtual IEnumerable<string> GenerateContentAboveMenu() =>
            FiggleFonts.SlantSmall.RenderIEnumerable("Typing Practice").Concat(
            FiggleFonts.KeyboardSmall.RenderIEnumerable(MenuTitle));
        
        public virtual IEnumerable<string> GenerateContentBelowMenu() => ["", "By: Daniel Aguirre"];

        #endregion

        #region Overrideable Methods For Formatting Menu Screen

        public virtual int GetOptionsBorderWidth() => 100;

        public virtual int GetOptionsBorderHeight() => 14;

        public virtual int GetCountOfOptionsToShow() => 3;

        public IEnumerable<string> FormatSelectedOption(string option) =>
            FiggleFonts.Doom.RenderIEnumerable(">  ")
            .MergeToRight(FiggleFonts.Doom.RenderIEnumerable(option))
            .MergeToRight(FiggleFonts.Doom.RenderIEnumerable("  <"));

        public IEnumerable<string> FormatNonSelectedOption(string option) =>
            FiggleFonts.CyberMedium.RenderIEnumerable(option);

        #endregion

        #region Implementation For Printing Menu And Selecting Next Screen

        public override Screen DisplayScreenAndGetNext()
        {
            Console.Clear();

            var options = GetOptions();
            var optionMessages = options.Select(_ => _.OptionMessage);
            var screens = options.Select(_ => _.NextScreen);

            var currentIndex = 0;
            var maxIndex = options.Count() - 1;
            var selectionMade = false;

            while (!selectionMade)
            {
                Console.SetCursorPosition(0, 0);

                _consoleWriter.WriteLines(_contentBuilder.New(_ => _.AddContent(
                    Center.HorizontalByConsoleWidth(
                        GenerateContentAboveMenu(),
                        GenerateMenuOptions(optionMessages, currentIndex),
                        GenerateContentBelowMenu()
                ))));

                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.Enter: selectionMade = true;
                    break;

                    // 0 goes to max, everything else goes down
                    case ConsoleKey.UpArrow: currentIndex = currentIndex == 0 ? maxIndex : --currentIndex;
                    break;

                    // max goes to 0, everything else goes up
                    case ConsoleKey.DownArrow: currentIndex = currentIndex == maxIndex ? 0 : ++currentIndex;
                    break;

                    default:
                    break;
                }
            }

            return options.ElementAt(currentIndex).NextScreen;
        }

        #endregion

        #region Private Method For Logic To Generate Menu Options

        private IEnumerable<string> GenerateMenuOptions(IEnumerable<string> options, int currentIndex)
        {
            var selectedOption = options.ElementAt(currentIndex);

            IEnumerable<string> optionsBeforeSelectedOption, optionsAfterSelectedOption;

            if (currentIndex == 0)
            {
                optionsBeforeSelectedOption = [];
                optionsAfterSelectedOption = options.Skip(1).Take(GetCountOfOptionsToShow() - 1);
            }
            else if (currentIndex == options.Count() - 1)
            {
                optionsBeforeSelectedOption = options.TakeLast(GetCountOfOptionsToShow()).SkipLast(1);
                optionsAfterSelectedOption = [];
            }
            else
            {
                var firstElementToShow = currentIndex - (GetCountOfOptionsToShow() / 2);

                optionsBeforeSelectedOption = options.Skip(firstElementToShow).Take(GetCountOfOptionsToShow() / 2);

                var countOfOptionsBeforeSelectedOption = optionsBeforeSelectedOption.Count();

                optionsAfterSelectedOption = options.Skip(currentIndex + 1).Take(GetCountOfOptionsToShow() - (countOfOptionsBeforeSelectedOption + 1));
            }

            return Border.Apply(GetOptionsBorderWidth(), Center.Vertical(GetOptionsBorderHeight(),
                optionsBeforeSelectedOption.Any() ? optionsBeforeSelectedOption.Select(FormatNonSelectedOption).Aggregate((a, b) => a.Concat(b)) : [],
                FormatSelectedOption(selectedOption),
                optionsAfterSelectedOption.Any() ? optionsAfterSelectedOption.Select(FormatNonSelectedOption).Aggregate((a, b) => a.Concat(b)) : []
            ));
        }

        #endregion
    }
}

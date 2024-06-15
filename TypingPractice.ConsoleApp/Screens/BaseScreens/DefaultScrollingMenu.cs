using Figgle;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class DefaultScrollingMenu : ScrollingMenu
    {
        public override long RefreshRatioInMilliseconds => 100;

        private ConsoleColor _selectionIndicatorColor = PrimaryFontColor;

        private string _leftSelectionIndicator = " >  ";

        private string _rightSelectionIndicator = "  < ";

        private bool _moveSelectionIndicatorInwards = true;

        public override void TriggerRefresh()
        {
            base.TriggerRefresh();

            _selectionIndicatorColor = _selectionIndicatorColor.CycleColor();

            if (RefreshCount % 3 == 0)
            {
                if (_moveSelectionIndicatorInwards)
                {
                    _leftSelectionIndicator = _leftSelectionIndicator switch
                    {
                        ">   " => " >  ",
                        " >  " => "  > ",
                        "  > " => "   >",
                        _ => ">   ",
                    };

                    _rightSelectionIndicator = _rightSelectionIndicator switch
                    {
                        "   <" => "  < ",
                        "  < " => " <  ",
                        " <  " => "<   ",
                        _ => "   <",
                    };

                    _moveSelectionIndicatorInwards = !(_leftSelectionIndicator == "  > ");
                }
                else
                {
                    _leftSelectionIndicator = _leftSelectionIndicator switch
                    {
                        "   >" => "  > ",
                        "  > " => " >  ",
                        " >  " => ">   ",
                        _ => ">   ",
                    };

                    _rightSelectionIndicator = _rightSelectionIndicator switch
                    {
                        "<   " => " <  ",
                        " <  " => "  < ",
                        "  < " => "   <",
                        _ => "   <",
                    };

                    _moveSelectionIndicatorInwards = !(_leftSelectionIndicator == " >  ");
                }
            }
        }

        public override int CountOfOptionsToShow => 3;

        public virtual int GetOptionsBorderWidth() => 70;

        public virtual int GetOptionsBorderHeight() => 16;

        public virtual DisplayedSection Header() => new(
            new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Typing Practice"),
            new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall, MenuTitle)
        );

        public virtual DisplayedSection Footer() => [];

        public abstract string MenuTitle { get; }

        public override DisplayedSection FormatNonSelectedOption(string option) => 
            new(SecondaryFontColor, BackgroundColor, FiggleFonts.CyberMedium, option);

        public override DisplayedSection FormatSelectedOption(string option) =>
            new DisplayedSection(_selectionIndicatorColor, BackgroundColor, FiggleFonts.Doom, _leftSelectionIndicator)
            .AddRightSideSection(BackgroundColor, 
                new(PrimaryFontColor, BackgroundColor, FiggleFonts.Doom, option))
            .AddRightSideSection(BackgroundColor, 
                new(_selectionIndicatorColor, BackgroundColor, FiggleFonts.Doom, _rightSelectionIndicator));

        public override DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions) => 
            new DisplayedSection(
                Header(),
                new BasicBorder(PrimaryBorderColor, BackgroundColor, GetOptionsBorderWidth(), GetOptionsBorderHeight(), 
                    formattedOptions).ToDisplayedSection(),
                Footer()
            ).Centered(BackgroundColor);
    }
}

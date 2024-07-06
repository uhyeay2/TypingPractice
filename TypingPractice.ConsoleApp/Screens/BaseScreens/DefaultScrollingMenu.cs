using Figgle;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.Animation;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class DefaultScrollingMenu : ScrollingMenu
    {
        private readonly Signature _signature = new(BackgroundColor);

        private readonly ColorChangingArrowGoingRight _leftSideSelectedOptionIndicator = new(BackgroundColor);

        private readonly ColorChangingArrowGoingLeft _rightSideSelectedOptionIndicator = new(BackgroundColor);

        public override IEnumerable<Animation> GetAnimations() => [_signature, _leftSideSelectedOptionIndicator, _rightSideSelectedOptionIndicator];

        public virtual int GetOptionsBorderWidth() => 120;

        public virtual int GetOptionsBorderHeight() => 17;

        private const int OptionsFontHeight = 6;

        public virtual DisplayedSection Header() => new(
            new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Typing Practice"),
            new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.KeyboardSmall, MenuTitle)
        );

        public virtual DisplayedSection Footer() => _signature.Display();

        public abstract string MenuTitle { get; }

        public override DisplayedSection FormatNonSelectedOption(string option) =>
            new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, option).CenteredVertical(BackgroundColor, OptionsFontHeight);

        public override DisplayedSection FormatSelectedOption(string option) =>
            _leftSideSelectedOptionIndicator.Display()
            .AddRightSideSection(BackgroundColor, new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, option)
                .CenteredVertical(BackgroundColor, OptionsFontHeight))
            .AddRightSideSection(BackgroundColor, _rightSideSelectedOptionIndicator.Display());

        public override DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions) => 
            new DisplayedSection(
                Header(),
                new DisplayInsideSimpleBorder(PrimaryBorderColor, BackgroundColor, GetOptionsBorderWidth(), GetOptionsBorderHeight(), 
                    formattedOptions),
                Footer()
            ).Centered(BackgroundColor);
    }
}

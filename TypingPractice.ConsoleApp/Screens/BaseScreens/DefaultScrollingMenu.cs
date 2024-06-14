using Figgle;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Display.BorderedSection;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class DefaultScrollingMenu : ScrollingMenu
    {
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
            new DisplayedSection(SecondaryFontColor, BackgroundColor, FiggleFonts.Doom, "> ")
            .AddRightSideSection(BackgroundColor, new(PrimaryFontColor, BackgroundColor, FiggleFonts.Doom, option))
            .AddRightSideSection(BackgroundColor, new(SecondaryFontColor, BackgroundColor, FiggleFonts.Doom, " <"));

        public override DisplayedSection GetDisplayedContent(DisplayedSection formattedOptions) => 
            new DisplayedSection(
                Header(),
                new BasicBorder(PrimaryBorderColor, BackgroundColor, GetOptionsBorderWidth(), GetOptionsBorderHeight(), 
                    formattedOptions).ToDisplayedSection(),
                Footer()
            ).Centered(BackgroundColor);
    }
}

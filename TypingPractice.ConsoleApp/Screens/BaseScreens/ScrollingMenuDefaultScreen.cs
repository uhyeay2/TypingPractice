using Figgle;
using TypingPractice.ConsoleApp.UserInterface;
using TypingPractice.ConsoleApp.Extensions;

namespace TypingPractice.ConsoleApp.Screens.BaseScreens
{
    public abstract class ScrollingMenuDefaultScreen : ScrollingMenuScreen
    {
        public override int CountOfOptionsToShow => 3;

        public virtual int GetOptionsBorderWidth() => 90;

        public virtual bool IsStrictOptionsBorderWidth() => true;

        public virtual int? GetOptionsBorderTargetHeight() => 15;

        public abstract string MenuTitle { get; }

        private const ConsoleColor SecondaryFontColor = ConsoleColor.Cyan;

        private static readonly IEnumerable<DisplayedLine> Footer = [
            new DisplayedLine(" ", BackgroundColor, BackgroundColor),
            new DisplayedLine("By: Daniel Aguirre", FontColor, BackgroundColor)
        ];

        public override IEnumerable<DisplayedLine> FormatNonSelectedOption(string option) =>
            FiggleFonts.CyberMedium.Render(SecondaryFontColor, BackgroundColor, option);

        public override IEnumerable<DisplayedLine> FormatSelectedOption(string option) => 
            FiggleFonts.Doom.Render(SecondaryFontColor, BackgroundColor, "> ")
            .MergeOnRightSide(FiggleFonts.Doom.Render(FontColor, BackgroundColor, option)).ToArray()
            .MergeOnRightSide(FiggleFonts.Doom.Render(SecondaryFontColor, BackgroundColor, " <"));

        public override DisplayedContent GetDisplayedContent(IEnumerable<DisplayedLine> formattedOptions) => new(
            FiggleFonts.SlantSmall.Render(FontColor, BackgroundColor, "Typing Practice")
            .Concat(FiggleFonts.KeyboardSmall.Render(SecondaryFontColor, BackgroundColor, MenuTitle))
            .Concat(_defaultBorder.Apply(GetOptionsBorderWidth(), IsStrictOptionsBorderWidth(), GetOptionsBorderTargetHeight(), formattedOptions))
            .Concat(Footer)
            .PadToCenterOfConsole(BackgroundColor));
    }
}

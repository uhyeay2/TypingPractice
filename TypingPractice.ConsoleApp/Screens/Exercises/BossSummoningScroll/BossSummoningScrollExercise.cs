using Figgle;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens.BaseScreens;

namespace TypingPractice.ConsoleApp.Screens.Exercises.BossSummoningScroll
{
    public class BossSummoningScrollExercise : ExerciseScreen
    {
        private readonly IEnumerable<string> _pool;

        private const ConsoleColor ScrollBorderColor = ConsoleColor.DarkYellow;

        private const int ScrollBorderWidth = 100;

        private const int ScrollBorderHeight = 20;

        private FireCauldronAnimation _fireCauldron = new();

        public BossSummoningScrollExercise(IEnumerable<string> pool)
        {
            _pool = pool;
        }

        public override DisplayedSection GetDisplayToPrintBeforeStarting() => new (
            new BigScrollBorder(ScrollBorderColor, BackgroundColor, ScrollBorderWidth, ScrollBorderHeight,
                new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.SlantSmall, "Typing  Practice"),
                new DisplayedSection(
                    _fireCauldron.AsDisplayedSection()
                        .AddRightSideSection(BackgroundColor, new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.Doom, "   BOSS   "))
                        .AddRightSideSection(BackgroundColor, _fireCauldron.AsDisplayedSection()),
                    new DisplayedSection(ConsoleColor.Red, BackgroundColor, FiggleFonts.Doom, "SUMMONING")
                ),
                new DisplayedSection(PrimaryFontColor, BackgroundColor, FiggleFonts.CyberMedium, "By: Daniel Aguirre")
            ).ToDisplayedSection().Centered(BackgroundColor)
        );

        public override DisplayedSection GetDisplay()
        {
            throw new NotImplementedException();
        }

        public override Screen GetNextScreen()
        {
            throw new NotImplementedException();
        }

        public override void ProcessInput(ConsoleKeyInfo input)
        {
            base.ProcessInput(input);
        }

        public override void StartExerciseLoop()
        {
            Console.ReadKey();
        }

        public override void StopExercise()
        {
            base.StopExercise();
        }

        public override IEnumerable<Animation> GetAnimations() => [_fireCauldron];
    }
}

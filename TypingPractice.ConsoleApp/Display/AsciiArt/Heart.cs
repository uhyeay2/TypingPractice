using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.AsciiArt
{
    public class Heart : DisplayedSection
    {        
        public Heart(ConsoleColor heartColor, ConsoleColor heartBorderColor, ConsoleColor backgroundColor)
        {
            foreach (var line in _heart)
            {
                Add(new DisplayedLine(
                    line.Select(_ => 
                        new DisplayedString(_, _ == '@' ? heartBorderColor : heartColor, backgroundColor)
                    ).ToList()
                ));
            }
        }

        private static readonly string[] _heart = [
          " @@@  @@@ ",
          "@###@@##%@",
          " @#####%@ ",
          "  @###%@  ",
          "   @#%@   ",
          "    @@    ",           
        ];
    }
}

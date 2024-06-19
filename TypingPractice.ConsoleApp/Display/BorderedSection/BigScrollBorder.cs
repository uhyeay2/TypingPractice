using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.BorderedSection
{
    internal class BigScrollBorder : DisplayedBorder
    {
        private const int MinimumHeightOfTopBorder = 5;

        private const int WidthOfMiddleSideBorders = 20;

        private readonly ConsoleColor _borderColor;

        private readonly ConsoleColor _backgroundColor;

        private DisplayedSection _insideTopSection, _insideBottomSection;

        public BigScrollBorder(ConsoleColor borderColor, ConsoleColor backgroundColor, int width, int height, DisplayedSection? insideTopSection, DisplayedSection insidemiddleSection, DisplayedSection? insideBottomSection) 
            : base(width, height, insidemiddleSection)
        {
            _borderColor = borderColor;

            _backgroundColor = backgroundColor;

            _insideTopSection = insideTopSection ?? [];

            _insideBottomSection = insideBottomSection ?? [];
        }

        /*
        
           ,____________________________________________________________.-``-._                  
          /`                                                         /` ._--_. `\                  
         /`                                                         /` /  ._ `\ `\                 
        |                                                          | ./  /  `| | |                
        |                                                          | | `| .| | | |                 
        |                                                          | `\ `\ ./`/ /`                  
        `\                                                         `\ `\ `\__/ /`                  
         `\,_________________________________________________________\ `\_____/`                  
           |                                                         `| |                  
           |                                                          | |                  
           |                                                          | |                  
           |                                                          | |                  
           |                                                          | |                  
           |                                                          | |                  
           |                                                          | |                  
           |                                                          | |                     
           |                                                          | |                 
           |                                                          | |              
           |                                                          | |           
           |                                                          | |        
           |                                                          | |      
          ,|_________________________________________________________/` |      
         /`                                                         /` ,``-.   
        /`                                                         /` / .-- `\ 
       |                                                           | | / . `\ ` 
       |                                                           | | | `| | |  <-- This line can duplicate, allow the bottom section to be larger
       |                                                           | \ `../ | |
       `\                                                          `\ \____/ /
        `\__________________________________________________________,\______/      

         /*
             
             /` / .-- `\ 
             | | / . `\ `

             | | | `| | | <-- Repeating Section

             | | | `| | |
             | \ `../ | |
             `\ \____/ /
             _,\______/  
             
             
             */
         
        protected override DisplayedSection GetBottomBorder()
        {
            var topLeft = new DisplayedSection(_borderColor, _backgroundColor,
                "  /`",
                " /` ",
                "|   ");

            var bottomLeft = new DisplayedSection(_borderColor, _backgroundColor,
                "|   ",
                "|   ",
                "`\\  ",
                " `\\_");

            var topRight = new DisplayedSection(_borderColor, _backgroundColor,                
                "  /` ,``-.      ",
                " /` / .-- `\\    ",
                " | | / . `\\ `   "
            );

            var bottomRight = new DisplayedSection(_borderColor, _backgroundColor,
                " | | | `| | |   ",
                " | \\ `../ | |   ",
                " `\\ \\____/ /    ",
                "__,\\______/     "
            );

            const int paddingForHeightOfTopAndBottomBorder = 2;

            while (topLeft.Count + bottomLeft.Count - paddingForHeightOfTopAndBottomBorder < _insideBottomSection.Count)
            {
                topLeft.Add(new("|   ", _borderColor, _backgroundColor));
                topRight.Add(new("| | | `| | | ", _borderColor, _backgroundColor));
            }

            var middleWidth = _width - topLeft.First().Width - topRight.First().Width;

            var firstLineLeftSide = "  \\,_";
            var firstLineRightSide = "/` |         ";
            var firstRowMiddleWidth = _width - firstLineLeftSide.Length - firstLineRightSide.Length;

            var firstRow = new DisplayedLine(firstLineLeftSide + new string('_', firstRowMiddleWidth) + firstLineRightSide, _borderColor, _backgroundColor);

            var topMiddle = new DisplayedSection(_borderColor, _backgroundColor, new string(' ', middleWidth));

            var bottomMiddle = new DisplayedSection(_borderColor, _backgroundColor, new string('_', middleWidth));

            var border = new DisplayedSection(topLeft, bottomLeft)
               .AddRightSideSection(_backgroundColor, 
                    new DisplayedSection(topMiddle, _insideBottomSection.CenteredVertical(_backgroundColor, MinimumHeightOfTopBorder), bottomMiddle)
                    .CenteredHorizontal(_backgroundColor, middleWidth))
               .AddRightSideSection(_backgroundColor, new DisplayedSection(topRight, bottomRight));

            border.Insert(0, firstRow);

            return border;

        }


        protected override DisplayedSection GetContentWithSideBorders() => 
            new (_insideSection.CenteredHorizontal(_backgroundColor, _width - WidthOfMiddleSideBorders)
                .Select(insideLine => MiddleLeftBorder() + insideLine + MiddleRightBorder()).ToArray());

        private DisplayedLine MiddleLeftBorder() => new("  | ", _borderColor, _backgroundColor);
        private DisplayedLine MiddleRightBorder() => new("    | |         ", _borderColor, _backgroundColor);

        protected override DisplayedSection GetTopBorder()
        {

            var topLeft = new DisplayedSection(_borderColor, _backgroundColor,
                "   ,",
                "  /`",
                " /` ",
                "|   ");

            var bottomLeft = new DisplayedSection(_borderColor, _backgroundColor,
                "|   ",
                "|   ",
                "`\\  ",
                " `\\,",
                "  | ");

            var topRight = new DisplayedSection(_borderColor, _backgroundColor,
               "______.-``-._   ",
               "   /` ._--_. `\\ ",
               "  /` /  ._ `\\ `\\",
               " | ./  /  `| | |"
            );

            var bottomRight = new DisplayedSection(_borderColor, _backgroundColor,
               " | | `| .| | | |",
               " | `\\ `\\ ./`/ /`",
               " `\\ `\\ `\\__/ /` ",
               "___\\ `\\_____/`  ",
               "   `| |         ");

            const int paddingForHeightOfTopAndBottomBorder = 4;

            // Add padding to the left and right side if the content inside top border is taller than the border is by default.
            while (topLeft.Count + bottomLeft.Count - paddingForHeightOfTopAndBottomBorder < _insideTopSection.Count)
            {
                topLeft.Add(new("|   ", _borderColor, _backgroundColor));
                topRight.Add(new(" | | `| .| | | |", _borderColor, _backgroundColor));
            }

            var middleWidth = _width - topLeft.First().Width - topRight.First().Width;

            var topMiddle = new DisplayedSection(_borderColor, _backgroundColor,
                new string('_', middleWidth),
                new string(' ', middleWidth)
            );

            var bottomMiddle = new DisplayedSection(_borderColor, _backgroundColor,
                new string('_', middleWidth)
            );

            var border = new DisplayedSection(topLeft, bottomLeft)
                .AddRightSideSection(_backgroundColor,
                        new DisplayedSection(topMiddle, _insideTopSection.CenteredVertical(_backgroundColor, MinimumHeightOfTopBorder), bottomMiddle).CenteredHorizontal(_backgroundColor, middleWidth))
                .AddRightSideSection(_backgroundColor, new DisplayedSection(topRight, bottomRight));

            return border;
        }
    }
}

using System.Diagnostics.SymbolStore;
using TypingPractice.ConsoleApp.Constants;
using TypingPractice.ConsoleApp.Display.Animation;
using TypingPractice.ConsoleApp.Exceptions;
using TypingPractice.ConsoleApp.Screens;
using TypingPractice.ConsoleApp.Screens.Settings;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

Console.CursorVisible = false;

//double currentSpeed = 0;

//var speedincrement = 2;

//var gauge = new BigNumberGauge(ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.Black, maxThreshold: 100, () => currentSpeed);

//gauge.RestartTimer();

//while (!Console.KeyAvailable)
//{
//    if (gauge.IsReadyToUpdate)
//    {
//        gauge.ChangeAnimation();
//    }

//    var display = gauge.Display();

//    Console.SetCursorPosition(0, 0);

//    foreach (var line in display)
//    {
//        line.Write();
//    }

//    currentSpeed += speedincrement;
//}



// Testing Zone

//var backgroundColor = ConsoleColor.Gray;
//var borderColor = ConsoleColor.DarkGray;
//var fontColor = ConsoleColor.Black;

//var reaper = GrimReaper.AsDisplayedSection(fontColor, backgroundColor)
//                       .CenteredHorizontal(backgroundColor, 41)
//                       .CenteredVertical(backgroundColor, 22)
//                       .CenteredHorizontal(borderColor, 45)
//                       .CenteredVertical(borderColor, 24)
//                       .Centered(ConsoleColor.Black);

//foreach (var line in reaper)
//{
//    line.Write();
//}

//Console.ReadKey();

// End Testing Zone

// Gameplay

Screen screen = ConsoleSize.IsMeetingMinimumRequirements ? new OpeningScreen()
                                                         : new UpdateConsoleSizeScreen(new OpeningScreen());
var keepRunning = true;

while (keepRunning)
{
    try
    {
        if (screen is ClosingScreen)
        {
            keepRunning = false;
        }

        screen = screen.DisplayScreenAndGetNext();
    }
    catch (ConsoleSizeTooSmallException e)
    {
        screen = new UpdateConsoleSizeScreen(e.Screen);
    }
}

Console.Clear();

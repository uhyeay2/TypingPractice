using TypingPractice.ConsoleApp.Display.AsciiArt;
using TypingPractice.ConsoleApp.Screens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

Console.CursorVisible = false;

// Testing Zone

var reaper = GrimReaper.AsDisplayedSection(ConsoleColor.Black, ConsoleColor.DarkYellow)
                       .CenteredHorizontal(ConsoleColor.DarkYellow, 41)
                       .CenteredVertical(ConsoleColor.DarkYellow, 22)
                       .CenteredHorizontal(ConsoleColor.DarkGray, 45)
                       .CenteredVertical(ConsoleColor.DarkGray, 24)
                       .Centered(ConsoleColor.Black);

foreach (var line in reaper)
{
    line.Write();
}

Console.ReadKey();

// End Testing Zone



// Gameplay

Screen screen = new OpeningScreen();

var keepRunning = true;

while (keepRunning)
{
    if (screen is ClosingScreen)
    {
        keepRunning = false;
    }

   screen = screen.DisplayScreenAndGetNext();
}

Console.Clear();

using TypingPractice.ConsoleApp.Screens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

Console.CursorVisible = false;

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

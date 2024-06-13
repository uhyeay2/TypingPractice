using TypingPractice.ConsoleApp.Display.BorderedSection;
using TypingPractice.ConsoleApp.Display.ScreenContent;
using TypingPractice.ConsoleApp.Screens;
using TypingPractice.ConsoleApp.Screens.SplashScreens;

Console.CursorVisible = false;

var displayedScreen = new DisplayedScreen(
    new BasicBorder(ConsoleColor.Black, ConsoleColor.DarkGreen, 20, 6,
        new(ConsoleColor.Black, ConsoleColor.DarkGreen, Figgle.FiggleFonts.SlantSmall, "Yo!")
    ).ToDisplayedSection()
    .Centered(ConsoleColor.DarkGreen, 22, 7)
    .Centered(ConsoleColor.Magenta, 26, 9)
    .Centered(ConsoleColor.Black, 30, 11)
    .Centered(ConsoleColor.Magenta, 34, 13)
    .Centered(ConsoleColor.Black)
    );

displayedScreen.Write();

Console.ReadKey();

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

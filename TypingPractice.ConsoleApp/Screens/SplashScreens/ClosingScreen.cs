﻿using Figgle;
using TypingPractice.ConsoleApp.Extensions;
using TypingPractice.ConsoleApp.Screens.BaseScreens;
using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Screens.SplashScreens
{
    public class ClosingScreen : AnyKeyToContinue
    {
        public override ScreenContent GetScreenContent() => _contentBuilder.New(_ => 
            _.AddContent(
                FiggleFonts.SlantSmall.RenderIEnumerable(
                    "Typing  Practice"),
                FiggleFonts.KeyboardSmall.RenderIEnumerable(
                    "THANK YOU ", 
                    "FOR PLAYING ",
                    "TYPING PRACTICE",
                    "COME BACK SOON!"),
                FiggleFonts.Italic.RenderIEnumerable(
                    "By:  Daniel  Aguirre"))
            .ApplyBorder()
            .CenterByConsoleSize(vertical: false));

        public override Screen GetNextScreen() => this;
    }
}

using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.AsciiArt
{
    public static class GrimReaper
    {
        public static DisplayedSection AsDisplayedSection(ConsoleColor fontColor, ConsoleColor backgroundColor) => new(fontColor, backgroundColor, _art);

        private static readonly string[] _art = [
            @"                                     ",
            @"               :&&&&&&               ",
            @"             &&       ;&$            ", 
            @"           &&  x&    &   &&          ", 
            @"         :&  ;& &&&&&&.&  +&         ", 
            @"        +&  &x&;&     &$&X  &        ", 
            @"       :&  &:& &&      +&x&  &       ", 
            @"      :&  &&    &   &$   & &  &      ", 
            @"     X& & &  &&&&&  &&&&& $ &  &     ", 
            @"    &$ & &: &&&&&&  &&&&&; & X  &    ", 
            @"   &; & &&  &&&&&   &&&&&x x& :: &$  ", 
            @"  &  & &&&    ;$  &&  X    &&&  + && ", 
            @" &  & &&&&&&&&.  &&&&  X&&&&&&&x & &&", 
            @"&& & &&&&&& &&         && &&&&&&+ & &", 
            @"&$; .&&&&&&  &&&&&& &&&&& &&&&&&& & &", 
            @" && .&&&&&&&   &&&&&&&&:  &&&&&&& &&&", 
            @"  &&  &&&&&&&           &&&&&&&& $&. ", 
            @"     &; .&&&&&&&      &&&&&&&  &$    ", 
            @"           +x&&&&&&&&&&&Xx:          ", 
            @"               x&&&&&&               ", 
            @"                  &$                 ", 
        ];
    }
}

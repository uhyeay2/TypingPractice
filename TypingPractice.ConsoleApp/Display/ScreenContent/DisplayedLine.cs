using System.Diagnostics;
using System.Text;

namespace TypingPractice.ConsoleApp.Display.ScreenContent
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class DisplayedLine : List<DisplayedString>
    {
        public DisplayedLine(IEnumerable<DisplayedString> strings)
        {
            AddRange(strings);
        }

        public DisplayedLine(string value, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Add(new(value, fontColor, backgroundColor));
        }

        public DisplayedLine(char value, int count, ConsoleColor fontColor, ConsoleColor backgroundColor) : this(new(value, count), fontColor, backgroundColor) { }

        public int Width => this.Sum(_ => _.Value.Length);

        public void Write()
        {
            ForEach(_ => _.Write());

            Console.WriteLine();
        }

        public static DisplayedLine operator +(DisplayedLine a, DisplayedLine? b)
        {
            if (b == null)
            {
                return a;
            }

            a.AddRange(b);

            return a;
        }

        private string GetDebuggerDisplay()
        {
            var sb = new StringBuilder();

            foreach (var str in this)
            {
                sb.Append(str.Value);
            }

            return sb.ToString();
        }
    }
}

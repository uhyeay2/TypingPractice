using TypingPractice.ConsoleApp.UserInterface;

namespace TypingPractice.ConsoleApp.Borders
{
    public class StandardBorder : BaseBorder
    {
        #region Constructor

        public StandardBorder(ConsoleColor borderColor, ConsoleColor backgroundColor) : base(backgroundColor) => BorderColor = borderColor;

        #endregion

        #region Public Members (Border/Background Colors)

        public ConsoleColor BorderColor { get; set; }

        #endregion

        #region Override Methods For Building Border

        public override int WidthOfSideBorders => _leftBorder.Length + _rightBorder.Length;

        public override IEnumerable<DisplayedLine> GetTopBorder(int targetWidth) => 
            [new(DisplayedText(" _" + new string('_', targetWidth) + "_ "))];

        public override IEnumerable<DisplayedLine> AddSideBorders(int targetWidth, IEnumerable<DisplayedLine> content) =>
            content.Select(_ => _.Prepend(DisplayedText(_leftBorder))
                                 .Append(DisplayedText(_rightBorder)));

        public override IEnumerable<DisplayedLine> GetBottomBorder(int targetWidth) => 
            [new(DisplayedText("|_" + new string('_', targetWidth) + "_|"))];

        #endregion

        #region Private Members Defining Border

        private const string _leftBorder = "| ";

        private const string _rightBorder = " |";

        private DisplayedText DisplayedText(string value) => new(value, BorderColor, BackgroundColor);

        #endregion
    }
}

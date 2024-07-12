using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    public class VerticalProgressBar : Animation
    {
        private readonly ConsoleColor _borderColor;
        private readonly ConsoleColor _backgroundColor;
        private ConsoleColor _progressBarColor;

        private const int _width = 7;
        private const int _heightOfTopAndBottomBorder = 2;
        private readonly int _height;
        private readonly int _maxValue;
        private readonly double _valueOfOneLine;

        private readonly Func<double> _funcGetCurrentValue;
        private double _heightOfProgressBarMissing;

        private DisplayedSection TopAndBottomBorder => new(new DisplayedLine(' ', _width, _borderColor, _borderColor));
        
        private DisplayedLine SideBorder => new DisplayedLine("  ", _borderColor, _borderColor);

        public VerticalProgressBar(ConsoleColor borderColor, ConsoleColor backgroundColor, int height, int maxValue, Func<double> funcGetCurrentValue)
        {
            _borderColor = borderColor;
            _backgroundColor = backgroundColor;

            _height = height;
            _maxValue = maxValue;

            _valueOfOneLine = (_maxValue * 1.00) / _height;

            _funcGetCurrentValue = funcGetCurrentValue;
        }

        private ConsoleColor GetProgressBarColor(double currentvalue) =>
        currentvalue switch
        {
            var r when r <= _maxValue / 3 => ConsoleColor.Red,
            var y when y <= (_maxValue / 3) * 2 => ConsoleColor.DarkYellow,
            _ => ConsoleColor.Green,
        };

        public override long RefreshRateInMilliseconds => 50;

        public override void ChangeAnimation()
        {
            // if maxValue is 0, we will not display the progress bar.
            if (_maxValue > 0)
            {
                var currentValue = _funcGetCurrentValue.Invoke();

                _progressBarColor = GetProgressBarColor(currentValue);

                _heightOfProgressBarMissing = double.Floor((_maxValue - currentValue) / _valueOfOneLine) - 1;
            }
        }

        // if maxValue is 0, we will not display the progress bar
        public override DisplayedSection Display()
        {
            if(_maxValue <= 0)
            {
                return [];
            }

            var progressBar = new List<DisplayedLine>();

            while (progressBar.Count < _heightOfProgressBarMissing)
            {
                var isAddingLastLineInProgressBar = progressBar.Count == _height - _heightOfTopAndBottomBorder - 1;

                if (isAddingLastLineInProgressBar)
                {
                    progressBar.Add(SideBorder + new DisplayedLine("===", _progressBarColor, _backgroundColor) + SideBorder);
                }
                else
                {
                    progressBar.Add(SideBorder + new DisplayedLine("   ", _backgroundColor, _backgroundColor) + SideBorder);
                }
            }

            while (progressBar.Count < _height - _heightOfTopAndBottomBorder)
            {
                progressBar.Add(SideBorder + new DisplayedLine("   ", _progressBarColor, _progressBarColor) + SideBorder);
            }

            return new(TopAndBottomBorder, progressBar, TopAndBottomBorder);
        }
    }
}

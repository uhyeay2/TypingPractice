using TypingPractice.ConsoleApp.Display.ScreenContent;

namespace TypingPractice.ConsoleApp.Display.Animation
{
    internal class BigNumberGauge : Animation
    {
        #region Private Members

        private readonly ConsoleColor _dialColor;

        private readonly ConsoleColor _borderColor;

        private readonly ConsoleColor _backgroundColor;

        private int _currentFrame;

        private readonly double _maxThreshold;

        private readonly Func<double> _funcGetCurrentSpeed;

        private static readonly string[][] _frames =
        [
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | |                               | | ",
                @" | |                               | | ",
                @" | |                               | | ",
                @" | |     ============              | | ",
                @" | | ==============/ \             | | ",
                @"  \ \_____________/   \____________/ / ",
                @"   \________________________________/  ",
            ],
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | |                               | | ",
                @" | |                               | | ",
                @" | |                               | | ",
                @" | | ================              | | ",
                @" | |     ==========/ \             | | ",
                @"  \ \_____________/   \____________/ / ",
                @"   \________________________________/  ",
            ],
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | |                               | | ",
                @" | |                               | | ",
                @" | | ==============                | | ",
                @" | |     ============              | | ",
                @" | |               / \             | | ",
                @"  \ \_____________/   \____________/ / ",
                @"   \________________________________/  ",
            ],
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | |                               | | ",
                @" | | =========                     | | ",
                @" | |     =========                 | | ",
                @" | |           ======              | | ",
                @" | |               / \             | | ",
                @"  \ \_____________/   \____________/ /  ",
                @"   \________________________________/   ",
            ],
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | | ======                        | | ",
                @" | |    ======                     | | ",
                @" | |       =======                 | | ",
                @" | |           ======              | | ",
                @" | |               / \             | | ",
                @"  \ \_____________/   \____________/ /  ",
                @"   \________________________________/   ",
            ],
            [
                @"    ________________________________   ",
                @"   / _____________________________  \  ",
                @"  / /                             \  \ ",
                @" | | ======                        | | ",
                @" | |    ======                     | | ",
                @" | |       =======                 | | ",
                @" | |           ======              | | ",
                @" | |               / \             | | ",
                @"  \ \_____________/   \____________/ /  ",
                @"   \________________________________/   ",
            ]
        ];


        #endregion

        #region Constructor 

        public BigNumberGauge(ConsoleColor dialColor, ConsoleColor borderColor, ConsoleColor backgroundColor, double maxThreshold, Func<double> funcGetCurrentValue)
        {
            _dialColor = dialColor;

            _borderColor = borderColor;

            _backgroundColor = backgroundColor;

            _maxThreshold = maxThreshold;

            _funcGetCurrentSpeed = funcGetCurrentValue;
        }

        #endregion

        #region Animation Overrides

        public override long RefreshRateInMilliseconds => 50;

        public override void ChangeAnimation()
        {
            var currentSpeed = _funcGetCurrentSpeed.Invoke();

            if (currentSpeed <= 0)
            {
                _currentFrame = 0;
            }
            else if (currentSpeed >= _maxThreshold)
            {
                _currentFrame = _frames.Length - 1;
            }
            else
            {
                var currentFrame = (int)double.Floor(currentSpeed / _maxThreshold * (_frames.Length - 1));

                _currentFrame = currentFrame >= _frames.Length ? _frames.Length - 1
                              : currentFrame <= 0 ? 0 : currentFrame;
            }
        }

        public override DisplayedSection Display()
        {
            var lines = new List<DisplayedLine>();

            foreach (var line in _frames[_currentFrame])
            {
                var displayedStrings = line.Select(_ => 
                    new DisplayedString(_, _ == '=' || _ == 'T' ? _dialColor : _borderColor, _backgroundColor));

                lines.Add(new DisplayedLine(displayedStrings));
            }

            return new DisplayedSection(lines);
        }

        #endregion

    }
}  
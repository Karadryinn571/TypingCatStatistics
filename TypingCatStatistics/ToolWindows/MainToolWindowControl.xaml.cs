using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace TypingCatStatistics
{
    public partial class MainToolWindowControl : UserControl
    {
        private readonly DispatcherTimer _timer;
        private readonly TypingSpeedometer _speedometer;
        private bool _catStatus = false;
        private readonly string _catUp = """
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣶⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣦⣄⣀⡀⣠⣾⡇⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀
            ⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⢿⣿⣿⡇⠀⠀⠀⠀
            ⠀⣶⣿⣦⣜⣿⣿⣿⡟⠻⣿⣿⣿⣿⣿⣿⣿⡿⢿⡏⣴⣺⣦⣙⣿⣷⣄⠀⠀⠀
            ⠀⣯⡇⣻⣿⣿⣿⣿⣷⣾⣿⣬⣥⣭⣽⣿⣿⣧⣼⡇⣯⣇⣹⣿⣿⣿⣿⣧⠀⠀
            ⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠸⣿⣿⣿⣿⣿⣿⣿⣷⠀
            """;
        private readonly string _catDown = """
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣷⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⣀⣶⣿⣿⣿⣿⣿⣿⣷⣶⣶⣶⣦⣀⡀⠀⢀⣴⣇⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀
            ⠀⠀⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀
            ⠀⠀⠀⣴⣿⣿⣿⣿⠛⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣄⠀⠀⠀
            ⠀⠀⣾⣿⣿⣿⣿⣿⣶⣿⣯⣭⣬⣉⣽⣿⣿⣄⣼⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀
            ⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄
            ⢸⣿⣿⣿⣿⠟⠋⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠁⣿⣿⣿⣿⡿⠛⠉⠉⠉⠉⠁
            ⠘⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠛⠃⠀⠀⠀⠀⠀⠀⠀
            """;
        private readonly string _statusTyping = "Typing";
        private readonly string _statusHolding = "Holding";
        private readonly double _averageMultiplier;
        private readonly double _instantMultiplier;

        public MainToolWindowControl()
        {
            InitializeComponent();

            _catIndicator.Text = _catDown;

            _timer = new();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += UpdateSpeed;
            _timer.Tick += UpdateStatus;
            _timer.Start();

            _speedometer = new(_timer, 5, 3);
            _speedometer.Change += UpdateCatIndicator;

            _averageMultiplier = 0.5;
            _instantMultiplier = 30;
        }

        private void UpdateCatIndicator(object sender, EventArgs args)
        {
            if (!_catStatus) _catIndicator.Text = _catUp;
            else _catIndicator.Text = _catDown;
            _catStatus = !_catStatus;
        }

        private void UpdateSpeed(object sender, EventArgs args)
        {
            var instantSpeed = (_speedometer.Speed * _averageMultiplier).ToString("f1");
            var instantTopSpeed = (_speedometer.TopSpeed * _averageMultiplier).ToString("f1");
            var averageSpeed = (_speedometer.Speed * _instantMultiplier).ToString("f1");
            var averageTopSpeed = (_speedometer.TopSpeed * _instantMultiplier).ToString("f1");
            _instantSpeed.Text = $"{instantSpeed}/{instantTopSpeed} char/sec";
            _averageSpeed.Text = $"{averageSpeed}/{averageTopSpeed} char/min";
        }

        private void UpdateStatus(object sender, EventArgs args)
        {
            if (_speedometer.Typing)
            {
                _status.Text = _statusTyping;
                _status.Foreground = Brushes.LimeGreen;
            }
            else
            {
                _status.Text = _statusHolding;
                _status.Foreground = Brushes.Red;
                _catIndicator.Text = _catDown;
                _catStatus = false;
            }
        }
    }
}
                      
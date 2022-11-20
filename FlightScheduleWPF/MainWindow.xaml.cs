using System.Windows;
using System.Windows.Input;

namespace FlightScheduleWPF
{
    public partial class MainWindow
    {
        private bool _isFullScreen;

        public MainWindow()
        {
            _isFullScreen = true;
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isFullScreen)
            {
                Topmost       = false;
                WindowState   = WindowState.Normal;
                WindowStyle   = WindowStyle.SingleBorderWindow;
                _isFullScreen = false;
            }
            else
            {
                Topmost       = true;
                WindowState   = WindowState.Maximized;
                WindowStyle   = WindowStyle.None;
                _isFullScreen = true;
            }
        }
    }
}
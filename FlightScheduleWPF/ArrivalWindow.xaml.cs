using System.Windows;
using System.Windows.Input;
using FlightScheduleWPF.ViewModels;

namespace FlightScheduleWPF
{
    public partial class ArrivalWindow : Window
    {
        private bool _isFullScreen;

        public ArrivalWindow(int numberOfWindow)
        {
            _isFullScreen = false;
            Title         = $"ArrivalWindow # {numberOfWindow +1}";
            DataContext   = new ArrivalWindowViewModel(numberOfWindow);
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
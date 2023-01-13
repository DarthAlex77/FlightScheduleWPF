using System;
using System.Timers;
using System.Windows;
using DevExpress.Mvvm;

namespace FlightScheduleWPF.ViewModels
{
    internal class MainWindowsViewModel : ViewModelBase
    {
        #region Constructor

        public MainWindowsViewModel()
        {
            TableLoadedCommand = new DelegateCommand<RoutedEventArgs>(TableLoaded);
            Timer clock = new Timer(1000);
            clock.AutoReset =  true;
            clock.Enabled   =  true;
            clock.Elapsed   += (_, _) => CurrentTime = DateTime.Now;
            clock.Start();
        }

        #endregion

        #region Methods

        public void SetStrings()
        {
            if (IsArrivalWindow)
            {
                WindowTypeRu = "Прилет";
                WindowTypeEn = "Arrival";
            }
            else
            {
                WindowTypeRu = "Вылет";
                WindowTypeEn = "Departure";
            }
        }

        #endregion

        #region Bindable Properties

        public string WindowName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string WindowTypeRu
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string WindowTypeEn
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public bool TwoColumnPerWindow
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool IsArrivalWindow
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public DateTime CurrentTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        #endregion

        #region Commands

        public DelegateCommand<RoutedEventArgs> TableLoadedCommand
        {
            get => GetValue<DelegateCommand<RoutedEventArgs>>();
            set => SetValue(value);
        }

        private void TableLoaded(RoutedEventArgs routedEventArgs)
        {
            int                   i       = FirstPageOfWindow + Count;
            TableControl          control = (TableControl) routedEventArgs.OriginalSource;
            TableControlViewModel vm      = (TableControlViewModel) control.DataContext;
            vm.IsArrivalWindow = IsArrivalWindow;
            vm.ItemsPerPage    = NumberOfStringPerWindow;
            vm.Pageindex       = i;
            vm.SetStrings();
            vm.GetFlightData();
            if (TwoColumnPerWindow)
            {
                Count++;
            }
        }

        #endregion

        #region Public Fields

        public int Count;
        public int NumberOfStringPerWindow;
        public int FirstPageOfWindow;

        #endregion
    }
}
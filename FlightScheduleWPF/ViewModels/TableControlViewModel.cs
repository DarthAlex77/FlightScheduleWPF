using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Data;
using DevExpress.Mvvm;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.ViewModels
{
    public class TableControlViewModel : ViewModelBase
    {
        #region Constructor

        public TableControlViewModel()
        {
            _lock   = new object();
            Flights = new ObservableCollection<Flight>();
            BindingOperations.EnableCollectionSynchronization(Flights, _lock);
            Timer data = new Timer(App.Settings.UpdateInterval.TotalMilliseconds);
            data.AutoReset =  true;
            data.Enabled   =  true;
            data.Elapsed   += (_, _) => { GetFlightData(); };
        }

        #endregion

        #region Bindable Properties

        public ObservableCollection<Flight> Flights
        {
            get => GetValue<ObservableCollection<Flight>>();
            set => SetValue(value);
        }

        #endregion

        #region Properties

        public int Pageindex
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
        public int ItemsPerPage
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public bool IsArrivalWindow
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        #endregion

        #region Methods

        public void SetStrings()
        {
            if (IsArrivalWindow)
            {
                _stringToday    = "Arrival";
                _stringTomorrow = "ArrivalTomorrow";
            }
            else
            {
                _stringToday    = "Departure";
                _stringTomorrow = "DepartureTomorrow";
            }
        }

        public void GetFlightData()
        {
            List<Flight> temp = new List<Flight>();
            temp.AddRange(Parser.ParseFlightData(_stringToday));
            temp.AddRange(Parser.ParseFlightData(_stringTomorrow));
            temp = new List<Flight>(temp.Where(flight => flight.TimeToEvent >= App.Settings.TimeToEventFilterStart || App.Settings.StatusesIgnoreFilter.Any(x => flight.Status == x)));
            lock (_lock)
            {
                Flights = new ObservableCollection<Flight>(temp.Skip(ItemsPerPage * Pageindex).Take(ItemsPerPage));
            }
        }

        #endregion

        #region Private Fields

        private          string _stringToday    = "Departure";
        private          string _stringTomorrow = "DepartureTomorrow";
        private readonly object _lock;

        #endregion
    }
}
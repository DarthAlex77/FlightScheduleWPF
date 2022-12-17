using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows.Data;
using DevExpress.Mvvm;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.ViewModels
{
    public class DepartureWindowViewModel : ViewModelBase
    {
        private readonly object _lock;
        private readonly int    _numberOfWindow;
        private          int    _countOfFlights;

        public DepartureWindowViewModel(int numberOfWindow)
        {
            _numberOfWindow = numberOfWindow;
            _countOfFlights = 0;
            _lock           = new object();
            Flights         = new ObservableCollection<Flight>();
            Timer clock = new Timer(1000);
            clock.AutoReset =  true;
            clock.Enabled   =  true;
            clock.Elapsed   += (_, _) => CurrentTime = DateTime.Now;
            clock.Start();
            GetFlightData();
            Timer data = new Timer(App.Settings.UpdateInterval.TotalMilliseconds);
            data.AutoReset =  true;
            data.Enabled   =  true;
            data.Elapsed   += (_, _) => { GetFlightData(); };
            data.Start();
            FlightView.Filter = filter => Filter((Flight) filter);
            BindingOperations.EnableCollectionSynchronization(Flights, _lock);
        }

        public DateTime CurrentTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public ObservableCollection<Flight> Flights { get; set; }
        public ICollectionView FlightView
        {
            get => CollectionViewSource.GetDefaultView(Flights);
        }

        private void GetFlightData()
        {
            lock (_lock)
            {
                Flights.Clear();
                _countOfFlights = 0;
                Flights.AddRange(Parser.ParseFlightData("Departure"));
                if (TimeOnly.FromDateTime(DateTime.Now) >= TimeOnly.FromTimeSpan(TimeSpan.FromHours(App.Settings.TomorrowParseStartHour)))
                {
                    Flights.AddRange(Parser.ParseFlightData("DepartureTomorrow"));
                }
            }
        }

        private bool Filter(Flight flight)
        {
            bool filter = flight.TimeToEvent >= App.Settings.TimeToEventFilterStart || App.Settings.StatusesIgnoreFilter.Any(x => flight.Status == x);
            if (filter)
            {
                _countOfFlights += flight.CodeSharesCount + 1;
                if (_countOfFlights > _numberOfWindow * App.Settings.NumberOfFlightsPerTable && _countOfFlights <= (_numberOfWindow + 1) * App.Settings.NumberOfFlightsPerTable)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Timers;
using System.Windows.Data;
using DevExpress.Mvvm;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly object _lock;

        public MainWindowViewModel()
        {
            IsDeparture = Settings.IsDeparture;
            _lock       = new object();
            Flights     = new ObservableCollection<Flight>();
            GetFlightData(null, null);
            Timer timer = new Timer();
            timer.Interval  =  Settings.UpdateInterval.TotalMilliseconds;
            timer.AutoReset =  true;
            timer.Enabled   =  true;
            timer.Elapsed   += GetFlightData;
            timer.Start();
            FlightView.Filter = o => Filter(o as Flight);
            BindingOperations.EnableCollectionSynchronization(Flights, _lock);
        }

        public bool                         IsDeparture { get; set; }
        public ObservableCollection<Flight> Flights     { get; set; }
        public ICollectionView FlightView
        {
            get => CollectionViewSource.GetDefaultView(Flights);
        }

        private void GetFlightData(object? sender, ElapsedEventArgs e)
        {
            lock (_lock)
            {
                Flights.Clear();
                Flights.AddRange(Parser.GetFlightData(Settings.ConnectionString));
                if (TimeOnly.FromDateTime(DateTime.Now)>=TimeOnly.FromTimeSpan(TimeSpan.FromHours(21)))
                {
                    Flights.AddRange(Parser.GetFlightData(Settings.TomorrowString));
                }
            }
        }

        private static bool Filter(Flight? flight)
        {
            return flight != null && flight.TimeToEvent >= Settings.TimeFilterStart && flight.TimeToEvent <= Settings.TimeFilterEnd || flight.Status == "Задержан";
        }
    }
}
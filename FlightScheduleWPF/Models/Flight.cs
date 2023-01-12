using System;
using DevExpress.Mvvm;

namespace FlightScheduleWPF.Models
{
    public class Flight : BindableBase
    {
        public Flight()
        {
            PlannedDateTime = DateTimeOffset.MinValue;
            ActualDt        = DateTimeOffset.MinValue;
            TimeToEvent     = TimeSpan.MinValue;
        }
        public DateTimeOffset PlannedDateTime
        {
            get => GetValue<DateTimeOffset>();
            set => SetValue(value);
        }
        public string Number
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string StationRu
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string StationEn
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public FlightStatus Status
        {
            get => GetValue<FlightStatus>();
            set => SetValue(value);
        }
        public DateTimeOffset? ActualDt
        {
            get => GetValue<DateTimeOffset?>();
            set => SetValue(value);
        }
        public TimeSpan TimeToEvent
        {
            get => GetValue<TimeSpan>();
            set => SetValue(value);
        }
        public string? CheckInDesks
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Gate
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public bool IsArrival
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

    }
}
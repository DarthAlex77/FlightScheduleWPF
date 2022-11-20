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
            TimeDifferent   = TimeSpan.MinValue;
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
        public string Station
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string Status
        {
            get => GetValue<string>();
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
        public TimeSpan TimeDifferent
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
    }
}
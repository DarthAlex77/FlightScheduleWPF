using System;

namespace FlightScheduleWPF.Models
{
    public static class Settings
    {
        public static string   ConnectionString { get; set; }
        public static string   TomorrowString   { get; set; }
        public static TimeSpan UpdateInterval   { get; set; }
        public static TimeSpan TimeFilterStart  { get; set; }
        public static TimeSpan TimeFilterEnd    { get; set; }
        public static bool     IsDeparture      { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace FlightScheduleWPF.Models
{
    public static class Settings
    {
        public static string       ConnectionString       { get; set; } = null!;
        public static string       TomorrowString         { get; set; } = null!;
        public static TimeSpan     UpdateInterval         { get; set; }
        public static TimeSpan     TimeFilterStart        { get; set; }
        public static TimeSpan     TimeFilterEnd          { get; set; }
        public static bool         IsDeparture            { get; set; }
        public static int          TomorrowParseStartHour { get; set; }
        public static List<string> StatusesIgnoreFilter   { get; set; } = null!;
    }
}
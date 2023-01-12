using System;
using System.Collections.Generic;

namespace FlightScheduleWPF.Models
{
    public class FlightScheduleConfig
    {
        public FlightScheduleConfig()
        {
            StatusesIgnoreFilter = new List<FlightStatus>();
            WindowSettings       = new List<WindowSettings>();
        }

        public List<WindowSettings> WindowSettings         { get; set; }
        public string               StationCode            { get; set; }
        public TimeSpan             UpdateInterval         { get; set; }
        public TimeSpan             TimeToEventFilterStart { get; set; }
        public List<FlightStatus>   StatusesIgnoreFilter   { get; set; }
    }
}
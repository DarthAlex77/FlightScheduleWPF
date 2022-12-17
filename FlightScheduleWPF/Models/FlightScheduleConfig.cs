using System;
using System.Collections.Generic;

namespace FlightScheduleWPF.Models
{
    public class FlightScheduleConfig
    {
        public FlightScheduleConfig()
        {
            StatusesIgnoreFilter = new List<FlightStatus>();
        }

        public string             ArrivalString            { get; set; }
        public string             ArrivalStringTomorrow    { get; set; }
        public string             DepartureString          { get; set; }
        public string             DepartureStringTomorrow  { get; set; }
        public int                NumberOfDepartureWindows { get; set; }
        public int                NumberOfArrivalWindows   { get; set; }
        public TimeSpan           UpdateInterval           { get; set; }
        public TimeSpan           TimeToEventFilterStart   { get; set; }
        public int                TomorrowParseStartHour   { get; set; }
        public int                NumberOfFlightsPerTable  { get; set; }
        public List<FlightStatus> StatusesIgnoreFilter     { get; set; }
    }
}
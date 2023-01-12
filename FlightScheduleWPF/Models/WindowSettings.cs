namespace FlightScheduleWPF.Models
{
    public class WindowSettings
    {
        public WindowSettings(bool isArrivalWindow, bool twoColumnPerWindow, int numberOfStringPerWindow)
        {
            IsArrivalWindow         = isArrivalWindow;
            TwoColumnPerWindow      = twoColumnPerWindow;
            NumberOfStringPerWindow = numberOfStringPerWindow;
        }

        public bool IsArrivalWindow         { get; set; }
        public bool TwoColumnPerWindow      { get; set; }
        public int  NumberOfStringPerWindow { get; set; }
    }
}
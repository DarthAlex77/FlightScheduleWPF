using System.Windows;
using System.Windows.Controls;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.Helpers
{
    internal class TableTypeSelector : DataTemplateSelector
    {
        public DataTemplate ArrivalRowTemplate   { get; set; } = null!;
        public DataTemplate DepartureRowTemplate { get; set; } = null!;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Flight flight)
            {
                if (flight.IsArrival)
                {
                    return ArrivalRowTemplate;
                }
                return DepartureRowTemplate;
            }
            return null!;
        }
    }
}
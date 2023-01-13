using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.Helpers
{
    public class StatusToTextConverterEn : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is FlightStatus status)
            {
                if (status != FlightStatus.Delayed)
                {
                    return GetAttributeOfType<DisplayAttribute>((Enum) values[0])!.Name!;
                }
                DateTimeOffset dt = (DateTimeOffset) values[1];
                return "Delayed to " + dt.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }
            throw new InvalidOperationException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static T? GetAttributeOfType<T>(Enum enumVal) where T : Attribute
        {
            Type         type       = enumVal.GetType();
            MemberInfo[] memInfo    = type.GetMember(enumVal.ToString());
            object[]     attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T) attributes[0] : null;
        }
    }
}
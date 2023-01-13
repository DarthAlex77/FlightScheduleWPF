using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using FlightScheduleWPF.Models;

namespace FlightScheduleWPF.Helpers
{
    public class StatusToTextConverterRu : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((FlightStatus) values[0] != FlightStatus.Delayed)
            {
                return GetDescription((Enum) values[0]);
            }
            DateTimeOffset dt = (DateTimeOffset) values[1];
            return "Задержан до " + dt.ToString("HH:mm");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string GetDescription(Enum en)
        {
            Type         type    = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute) attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }
}
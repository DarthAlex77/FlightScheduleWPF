using System.ComponentModel;

namespace FlightScheduleWPF.Models
{
    public enum FlightStatus
    {
        [Description("По расписанию")]
        OnTime=0,

        [Description("Прибыл")]
        Arrived=1,

        [Description("Прилетит раньше")]
        Early=2,

        [Description("Задержан")]
        Delayed=3,

        [Description("Отменен")]
        Cancelled=4,

        [Description("Нет данных")]
        Unknown=5,

        [Description("Вылетел")]
        Departed=6
    }
}
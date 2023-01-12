using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightScheduleWPF.Models
{
    public enum FlightStatus
    {
        [Display(Name = "On Time"), Description("По расписанию")]
        OnTime = 0,

        [Display(Name = "Arrived"), Description("Прибыл")]
        Arrived = 1,

        [Display(Name = "Early"), Description("Прилетит раньше")]
        Early = 2,

        [Display(Name = "Delayed"), Description("Задержан")]
        Delayed = 3,

        [Display(Name = "Cancelled"), Description("Отменен")]
        Cancelled = 4,

        [Display(Name = "Unknown"), Description("Нет данных")]
        Unknown = 5,

        [Display(Name = "Departed"), Description("Вылетел")]
        Departed = 6
    }
}
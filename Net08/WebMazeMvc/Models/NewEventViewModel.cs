using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMazeMvc.Models
{
    public class NewEventViewModel
    {
        public TypeOfEventEnum TypeOfEvent { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DayOfWeek DayOfWeekForMonthEvent { get; set; }
        public int DayOfMonth { get; set; }
        public int DayOfMonthForQuartal { get; set; }
        public NumberOfWeekOfMonthEnum NumberOfWeekOfMonth { get; set; }
        public MonthEnum Month { get; set; }
        public int NumberOfMonth { get; set; }
        public int DayOfQuartal { get; set; }
        [Required]
        public string EventText { get; set; }
        public TypeOfMonthEnum TypeOfMonth { get; set; }
        public TypeOfQuartalEnum TypeOfQuarter { get; set; }
        public int PeriodOfDays { get; set; }
        public DateTime DateTimeOfEvent { get; set; }




    }
}

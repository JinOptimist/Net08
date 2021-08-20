using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models;

namespace WebMazeMvc.EfStuff.Model
{
    public class Event: BaseModel
    {
        public TypeOfEventEnum TypeOfEvent { get; set; }
        public TypeOfMonthEnum TypeOfMonth { get; set; }
        public MonthEnum Month { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DayOfWeek DayOfWeekForMonthEvent { get; set; }
        public int DayOfMonth { get; set; }
        public NumberOfWeekOfMonthEnum NumberOfWeekOfMonth { get; set; }
        public int NumberOfMonth { get; set; }
        public int DayOfQuartal { get; set; }
        public string EventText { get; set; }
        public TypeOfQuartalEnum TypeOfQuarter { get; set; }
        public virtual User User { get; set; }
        public int PeriodOfDays { get; set; }
        public DateTime DateTimeOfEvent { get; set; }
        public int DayOfMonthForQuartal { get; set; }
    }
}

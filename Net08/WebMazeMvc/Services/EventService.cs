using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.Models;

namespace WebMazeMvc.Services
{
    public class EventService
    {
        private const int daysInWeek = 7;
        private const int numberOfMonthInQuartal = 3;

        private DayOfWeek dayOfWeekNow = DateTime.Today.DayOfWeek;
        private int dayOfMonthToday = DateTime.Today.Day;
        private int daysInThisMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

        private Dictionary<int, int> monthByQuartal = new Dictionary<int, int>()
        {
            {1,1 },
            {2,2 },
            {3,3 },
            {4,1 },
            {5,2 },
            {6,3 },
            {7,1 },
            {8,2 },
            {9,3 },
            {10,1 },
            {11,2 },
            {12,3 },
        };

        private List<DateTime> firstDaysOfQuartal = new List<DateTime>()
        {
            new DateTime (DateTime.Today.Year, 1,1),
            new DateTime (DateTime.Today.Year, 4,1),
            new DateTime (DateTime.Today.Year, 7,1),
            new DateTime (DateTime.Today.Year, 10,1)
        };

        private List<int> daysInQuartal = new List<int>()
        {
            DateTime.IsLeapYear(DateTime.Today.Year) == true ? 91 : 90,
            91,
            92,
            92
        };

        public bool WeekEvent(Event weekEvent)
        {
            if (dayOfWeekNow == weekEvent.DayOfWeek)
            {
                return true;
            }
            return false;
        }
        public bool MonthEvent(Event monthEvent)
        {
            if (monthEvent.TypeOfMonth == TypeOfMonthEnum.ByDayOfTheMonth)
            {
                monthEvent.DayOfMonth = monthEvent.DayOfMonth > daysInThisMonth == true ? daysInThisMonth : monthEvent.DayOfMonth;

                if (dayOfMonthToday == monthEvent.DayOfMonth)
                {
                    return true;
                }
                return false;
            }
            else if (monthEvent.TypeOfMonth == TypeOfMonthEnum.ByWeeksAndNameOfDay)
            {
                if (dayOfWeekNow != monthEvent.DayOfWeekForMonthEvent)
                {
                    return false;
                }

                var numberOfWeek = Math.Ceiling((double)(dayOfMonthToday / (double)daysInWeek));

                if (((int)monthEvent.NumberOfWeekOfMonth) == ((int)NumberOfWeekOfMonthEnum.last))
                {

                    numberOfWeek = (dayOfMonthToday + daysInWeek) > daysInThisMonth == true ? (int)monthEvent.NumberOfWeekOfMonth : numberOfWeek;
                }

                if (((int)monthEvent.NumberOfWeekOfMonth) == numberOfWeek)
                {
                    return true;
                }
            }
            return false;
        }
        public bool QuarterEvent(Event quartalEvent)
        {
            if (quartalEvent.TypeOfQuarter == TypeOfQuartalEnum.ByDay)
            {
                var numberOfQuartalNow = Math.Ceiling((double)DateTime.Today.Month / (double)numberOfMonthInQuartal);

                var dayOfQuartalNow = DateTime.Now.Subtract(firstDaysOfQuartal[(int)numberOfQuartalNow - 1]).Days;

                quartalEvent.DayOfQuartal = quartalEvent.DayOfQuartal > daysInQuartal[(int)numberOfQuartalNow]
                    == true
                    ? daysInQuartal[(int)numberOfQuartalNow]
                    : quartalEvent.DayOfQuartal;

                if (dayOfQuartalNow == quartalEvent.DayOfQuartal)
                {
                    return true;
                }

                return false;
            }
            else if (quartalEvent.TypeOfQuarter == TypeOfQuartalEnum.ByNumberOFMounth)
            {
                var numberOfMonthinQuartalToday = monthByQuartal.Single(x => x.Key == DateTime.Today.Month).Value;

                if (quartalEvent.NumberOfMonth != numberOfMonthinQuartalToday)
                {
                    return false;
                }
                if (quartalEvent.DayOfMonthForQuartal > daysInThisMonth)
                {
                    return true;
                }

                quartalEvent.DayOfMonthForQuartal = quartalEvent.DayOfMonthForQuartal > daysInThisMonth
                    == true
                    ? daysInThisMonth : quartalEvent.DayOfMonthForQuartal;

                if (quartalEvent.DayOfMonthForQuartal == DateTime.Today.Day)
                {
                    return true;
                }
            }
            return false;
        }
        public bool YearEvent(Event yearEvent)
        {
            return false;
        }
        public bool CustomEvent(Event customEvent)
        {
            var amountOfDays = DateTime.Now.Subtract(customEvent.DateTimeOfEvent).Days;

            if (amountOfDays % customEvent.PeriodOfDays == 0)
            {
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;

namespace WebMazeMvc.Services
{
    public class EventService
    {
        EventRepository _eventRepository { get; set; }
        UserService _userService { get; set; }

        public EventService(EventRepository eventRepository, UserService userService)
        {
            _eventRepository = eventRepository;
            _userService = userService;
        }

        private const int daysInWeek = 7;
        private const int numberOfMonthInQuartal = 3;

        private DayOfWeek dayOfWeekNow = DateTime.Today.DayOfWeek;
        private int dayOfMonthToday = DateTime.Today.Day;
        private int daysInThisMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        private double numberOfQuartalNow = Math.Ceiling((double)DateTime.Today.Month / (double)numberOfMonthInQuartal);

        public bool WeekEvent(Event weekEvent) => dayOfWeekNow == weekEvent.DayOfWeek;

        public bool MonthEvent(Event monthEvent)
        {
            switch (monthEvent.TypeOfMonth)
            {
                case TypeOfMonthEnum.ByDayOfTheMonth:

                    monthEvent.DayOfMonth = Math.Min(monthEvent.DayOfMonth, daysInThisMonth);

                    return dayOfMonthToday == monthEvent.DayOfMonth;

                case TypeOfMonthEnum.ByWeeksAndNameOfDay:

                    if (dayOfWeekNow != monthEvent.DayOfWeekForMonthEvent)
                    {
                        return false;
                    }

                    var numberOfWeek = Math.Ceiling((double)(dayOfMonthToday / (double)daysInWeek));

                    if (monthEvent.NumberOfWeekOfMonth == NumberOfWeekOfMonthEnum.last)
                    {
                        numberOfWeek = (dayOfMonthToday + daysInWeek) > daysInThisMonth ? (int)monthEvent.NumberOfWeekOfMonth : numberOfWeek;
                    }

                    if (((int)monthEvent.NumberOfWeekOfMonth) == numberOfWeek)
                    {
                        return true;
                    }
                    break;
            }
            throw new Exception("Error. Wrong type of month");
        }
        public bool QuarterEvent(Event quartalEvent)
        {
            switch (quartalEvent.TypeOfQuarter)
            { 
                case TypeOfQuartalEnum.ByDay:

                    var firstMonthOfQuartal =(int)(numberOfQuartalNow * numberOfMonthInQuartal - 2);

                    var dayOfQuartalNow = (DateTime.Now -new  DateTime(DateTime.Today.Year, firstMonthOfQuartal, 1)).Days;

                    var daysInQuartal = Enumerable.Range(0, 3)
                        .Select(x => DateTime.DaysInMonth(DateTime.Today.Year, (int)numberOfQuartalNow * numberOfMonthInQuartal - x))
                        .Sum();

                    quartalEvent.DayOfQuartal = Math.Min(quartalEvent.DayOfQuartal, daysInQuartal);

                    return dayOfQuartalNow == quartalEvent.DayOfQuartal;

                case TypeOfQuartalEnum.ByNumberOFMounth:

                    var numberOfMonthInQuartalToday = (int)DateTime.Today.Month - (numberOfQuartalNow - 1) * numberOfMonthInQuartal;

                    if (quartalEvent.NumberOfMonth != numberOfMonthInQuartalToday)
                    {
                        return false;
                    }

                    quartalEvent.DayOfMonthForQuartal = Math.Min(quartalEvent.DayOfMonthForQuartal, daysInThisMonth);

                    return quartalEvent.DayOfMonthForQuartal == DateTime.Today.Day;     
            }
            throw new Exception("Error. Wrong type of quartal");
        }
        public bool YearEvent(Event yearEvent)
        {
            return false;
        }
        public bool CustomEvent(Event customEvent)
        {
            var amountOfDays = (DateTime.Now - customEvent.DateTimeOfEvent).Days;

            return amountOfDays % customEvent.PeriodOfDays == 0;
        }
        public List<string> GetEventsToday()
        {
            var user =_userService.GetCurrent();
             
            var events = _eventRepository.GetAllUserEvent(user);

            var eventsName = new List<string>();

            foreach (var item in events)
            {
                switch (item.TypeOfEvent)
                {
                    case TypeOfEventEnum.week:
                        if (WeekEvent(item))
                        {
                            eventsName.Add(item.EventText);
                        };
                        break;
                    case TypeOfEventEnum.month:
                        if (MonthEvent(item))
                        {
                            eventsName.Add(item.EventText);
                        };
                        break;
                    case TypeOfEventEnum.quartal:
                        if (QuarterEvent(item))
                        {
                            eventsName.Add(item.EventText);
                        };
                        break;
                    case TypeOfEventEnum.year:
                        if (YearEvent(item))
                        {
                            eventsName.Add(item.EventText);
                        }

                        break;
                    case TypeOfEventEnum.custom:
                        if (CustomEvent(item))
                        {
                            eventsName.Add(item.EventText);
                        }
                        break;
                }  
            }
            return eventsName;
        }
    }
}

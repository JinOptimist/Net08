﻿using System;
using System.Collections.Generic;
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

                    var dayOfQuartalNow = (DateTime.Now - firstDaysOfQuartal[(int)numberOfQuartalNow - 1]).Days;

                    quartalEvent.DayOfQuartal = quartalEvent.DayOfQuartal > daysInQuartal[(int)numberOfQuartalNow]
                        == true
                        ? daysInQuartal[(int)numberOfQuartalNow]
                        : quartalEvent.DayOfQuartal;

                    return dayOfQuartalNow == quartalEvent.DayOfQuartal;

                case TypeOfQuartalEnum.ByNumberOFMounth:

                    var numberOfMonthinQuartalToday = (int)DateTime.Today.Month - (numberOfQuartalNow - 1) * numberOfMonthInQuartal;

                    if (quartalEvent.NumberOfMonth != numberOfMonthinQuartalToday)
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

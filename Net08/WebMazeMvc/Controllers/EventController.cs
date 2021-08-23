using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class EventController: Controller
    {
        private UserService _userService;
        private EventRepository _eventRepository;
        private EventService _eventService;
        private IMapper _mapper;

        public EventController(UserService userService,
            IMapper mapper,
            EventRepository eventRepository,
            EventService eventService)
        {
            _userService = userService;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _eventService = eventService;
        }
        public IActionResult EventCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EventCreate(NewEventViewModel newEvent)
        {
            var createdEvent = _mapper.Map<Event>(newEvent);

            createdEvent.User = _userService.GetCurrent();

            _eventRepository.Save(createdEvent);

            return RedirectToAction("EventCreate");
        }
        public IActionResult ShowEvent()
        {
            var viewModel = _eventService.GetEventsToday();

            return View(viewModel);
        }
        public IActionResult DaysInMonth(TypeOfMonthEnum name) // Недоделано
        {  
            return Json(65);
        }
    }
}

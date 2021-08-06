using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class ForumController : Controller
    {
        private ForumRepository _forumRepository;
        private UserService _userService;
        private IMapper _mapper;

        public ForumController(ForumRepository forumRepository, 
            UserService userService, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult All()
        {
            var user = _userService.GetCurrent();
            var allForums = _forumRepository.GetAll();
            var viewModels = _mapper.Map<List<MainForumViewModel>>(allForums);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = user != null && viewModel.UserId == user.Id;
            }

            return View(viewModels);
        }

        [Authorize]
        public IActionResult My()
        {
            var user = _userService.GetCurrent();
            var allForums = _forumRepository.GetAll()
                .Where(x => x.Creater == user);
            var viewModels = _mapper.Map<List<MainForumViewModel>>(allForums);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = true;
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddForumViewModel viewModel)
        {
            var user = _userService.GetCurrent();

            var forum = new Forum()
            {
                Topic = viewModel.Topic,
                DateCreated = DateTime.UtcNow,
                Creater = user
            };

            _forumRepository.Save(forum);

            return RedirectToAction("All");
        }
    }
}

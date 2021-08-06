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

        public ForumController(ForumRepository forumRepository, UserService userService)
        {
            _forumRepository = forumRepository;
            _userService = userService;
        }

        public IActionResult All()
        {
            var user = _userService.GetCurrent();

            var allForums = _forumRepository.GetAll();

            var viewModels = allForums
                .Select(x => new MainForumViewModel()
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    DateCreated = x.DateCreated,
                    NameCreater = x.Creater.Login,
                    CanEdit = x.Creater == user
                }).ToList();

            return View(viewModels);
        }

        [Authorize]
        public IActionResult My()
        {
            var user = _userService.GetCurrent();

            var allForums = _forumRepository.GetAll()
                .Where(x => x.Creater == user);

            var viewModels = allForums
                .Select(x => new MainForumViewModel()
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    DateCreated = x.DateCreated,
                    NameCreater = x.Creater.Login,
                    CanEdit = true
                }).ToList();

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

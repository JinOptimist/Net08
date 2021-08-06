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
            var allForums = _forumRepository.GetAll();

            var viewModels = allForums
                .Select(x => new MainForumViewModel()
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    DateCreated = x.DateCreated,
                    Creater = x.Creater
                }).ToList();

            return View(viewModels);
        }

        public IActionResult My()
        {
            var user = _userService.GetCurrent();

            if (user == null)
                return RedirectToAction("Login", "User");

            var allForums = _forumRepository.GetAll()
                .Where(x => x.Creater == user);

            var viewModels = allForums
                .Select(x => new MainForumViewModel()
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    DateCreated = x.DateCreated,
                    Creater = x.Creater
                }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddForumViewModel viewModel)
        {
            var user = _userService.GetCurrent();

            if (user == null)
                return RedirectToAction("Login", "User");

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

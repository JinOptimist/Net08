using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private NewsRepository _newsRepository;
        private CommentRepository _commentRepository;
        private UserService _userService;
        private IMapper _mapper;

        public ForumController(
            ForumRepository forumRepository,
            NewsRepository newsRepository,
            CommentRepository commentRepository,
            UserService userService, 
            IMapper mapper)
        {
            _forumRepository = forumRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult All()
        {
            var user = _userService.GetCurrent();
            var forums = _forumRepository.GetAll();
            var viewModels = _mapper.Map<List<MainForumViewModel>>(forums);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = viewModel.UserId == user?.Id;
            }

            return View(viewModels);
        }

        [Authorize]
        public IActionResult My()
        {
            var user = _userService.GetCurrent();
            var forums = _forumRepository.GetByUserId(user.Id);
            var viewModels = _mapper.Map<List<MainForumViewModel>>(forums);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = true;
            }

            return View(viewModels);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            var allNews = _newsRepository.GetWithoutForum();
            var viewModel = new AddForumViewModel();

            viewModel.AllNewsOptions =
                allNews
                .Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString(),
                })
                .ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddForumViewModel viewModel)
        {
            var user = _userService.GetCurrent();

            var forum = _mapper.Map<AddForumViewModel, Forum>(viewModel);
            forum.DateCreated = DateTime.UtcNow;
            forum.Creater = user;

            _forumRepository.Save(forum);

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            var forum = _forumRepository.Get(id);

            _forumRepository.Remove(forum);

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Generate(int count)
        {
            var user = _userService.GetCurrent();

            for (var i = 0; i < count; i++)
            {
                var news = new News()
                {
                    Title = $"Title News {i + 1}",
                    Creater = user
                };

                news.Forum = new Forum()
                {
                    Topic = $"Topic Forum {i + 1}",
                    NewsId = news.Id,
                    DateCreated = DateTime.UtcNow,
                    Creater = user,
                    Comments = new List<Comment>()  
                };

                for (var j = 0; j < 10; j++)
                {
                    var comment = new Comment()
                    {
                        Message = $"Message for Comment {j + 1}",
                        Forum = news.Forum,
                        DateCreated = DateTime.UtcNow,
                        Creater = user
                    };

                    news.Comments.Add(comment);
                }

                _newsRepository.Save(news);
            }

            return RedirectToAction("All");
        }
    }
}

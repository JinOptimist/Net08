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
            var allForums = _forumRepository.GetAll();
            var viewModels = _mapper.Map<List<MainForumViewModel>>(allForums);

            foreach (var viewModel in viewModels)
            {
                viewModel.CountComments = _commentRepository.GetAll()
                    .Where(x => x.Forum.Id == viewModel.Id)
                    .ToList()
                    .Count;
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
                viewModel.CountComments = _commentRepository.GetAll()
                    .Where(x => x.Forum?.Id == viewModel.Id)
                    .ToList()
                    .Count;
                viewModel.CanEdit = true;
            }

            return View(viewModels);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new AddForumViewModel();

            var allNews = _newsRepository.GetAll()
                .Where(x => x.Forum == null);

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

            var forum = new Forum()
            {
                Topic = viewModel.Topic,
                NewsId = viewModel.NewsId,
                DateCreated = DateTime.UtcNow,
                Creater = user
            };

            _forumRepository.Save(forum);

            return RedirectToAction("All");
        }
    }
}

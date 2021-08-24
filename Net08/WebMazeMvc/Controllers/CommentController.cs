﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class CommentController : Controller
    {
        private ForumRepository _forumRepository;
        private NewsRepository _newsRepository;
        private UserRepository _userRepository;
        private CommentRepository _commentRepository;
        private UserService _userService;
        private IMapper _mapper;

        public CommentController(
            ForumRepository forumRepository,
            NewsRepository newsRepository,
            CommentRepository commentRepository,
            UserService userService,
            UserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _forumRepository = forumRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult All()
        {
            var user = _userService.GetCurrent();
            var comments = _commentRepository.GetAll();
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(comments);

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
            var comments = _commentRepository.GetByUserId(user.Id);
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(comments);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = true;
            }

            return View(viewModels);
        }

        public IActionResult Get(long forumId)
        {
            var user = _userService.GetCurrent();
            var comments = _commentRepository.GetByForumId(forumId);
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(comments);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = viewModel.UserId == user?.Id;
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            var comment = _commentRepository.Get(id);

            _commentRepository.Remove(comment);

            return RedirectToAction("All");
        }
        [Authorize]
        public IActionResult All(long id)
        {
            var user = _userService.GetCurrent();
            var allCommentOnForum = _commentRepository.GetByForumId(id);
            var viewModels = _mapper.Map<List<AllCommentsViewModel>>(allCommentOnForum);
            foreach (var viewModel in viewModels)
            {
                foreach (var comment in allCommentOnForum)
                {
                    viewModel.CanDelete = user.Id == comment.Creater.Id;
                }
            }
            return View(viewModels);
        }
        [HttpPost]
        public IActionResult Add(CommentViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.CreaterId);

            var comment = _mapper.Map<Comment>(viewModel);

            _commentRepository.Save(comment);

            return RedirectToAction("All", "News");
        }
    }
}

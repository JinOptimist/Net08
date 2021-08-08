using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class CommentController : Controller
    {
        private ForumRepository _forumRepository;
        private NewsRepository _newsRepository;
        private CommentRepository _commentRepository;
        private UserService _userService;
        private IMapper _mapper;

        public CommentController(
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
            var allComments = _commentRepository.GetAll();
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(allComments);

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
            var allComments = _commentRepository.GetAll()
                .Where(x => x.Creater == user);
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(allComments);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = true;
            }

            return View(viewModels);
        }

        public IActionResult Get(long ForumId)
        {
            var user = _userService.GetCurrent();
            var allComments = _commentRepository.GetAll()
                .Where(x => x.Forum.Id == ForumId);
            var viewModels = _mapper.Map<List<MainCommentViewModel>>(allComments);

            foreach (var viewModel in viewModels)
            {
                viewModel.CanEdit = user != null && viewModel.UserId == user.Id;
            }

            return View(viewModels);
        }
    }
}

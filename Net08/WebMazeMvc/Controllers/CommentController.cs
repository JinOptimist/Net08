using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Controllers.AuthAttribute;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class CommentController : Controller
    {
        private CommentRepository _commentRepository;
        private UserRepository _userRepository;
        private IMapper _mapper;

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CommentViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.CreaterId);

            var comment = _mapper.Map<Comment>(viewModel);
           
            _commentRepository.Save(comment);

            return RedirectToAction("All", "News");
        }
        [Authorize]
        public IActionResult Remove(long id)
        {
            var comment = _commentRepository.Get(id);

            _commentRepository.Remove(comment);

            return RedirectToAction("All");
        }

        public IActionResult All(long id)
        {
            var allCommentOnForum = _commentRepository.GetAll().Where(x => x.Forum.Id == id);

            var viewModels = _mapper.Map<List<CommentViewModel>>(allCommentOnForum);

            return View(viewModels);
        }

        public IActionResult MyComments()
        {
            var user = _userService.GetCurrent();

            var viewModels = _mapper.Map<List<ShortNewsViewModel>>(user.NewsCreatedByMe);

            return View(viewModels);
        }
    }
}

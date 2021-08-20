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
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class CommentController : Controller
    {
        private CommentRepository _commentRepository;
        private UserRepository _userRepository;
        private UserService _userService;

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CommentViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.CreaterId);

            var comment = new Comment()
            {
                Message = viewModel.Message,
                Creater = user
            };
           
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
        [Authorize]
        public IActionResult All(long id)
        {
            var user = _userService.GetCurrent();
            var allCommentOnForum = _commentRepository.GetAll().Where(x => x.Forum.Id == id).ToList();
            var viewModels = new List<AllCommentsViewModel>();
            foreach (var comment in allCommentOnForum)
            {
                var viewModel = new AllCommentsViewModel()
                {
                    Message = comment.Message,
                    CreaterId = comment.Creater.Id,
                    UserId = user.Id
                };
                viewModels.Add(viewModel);
            }
            

            return View(viewModels);
        }
    }
}

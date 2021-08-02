using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository _newsRepository;
        private UserRepository _userRepository;

        public NewsController(NewsRepository newsRepository, 
            UserRepository userRepository)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            var allNews = _newsRepository.GetAll();

            var viewModels = allNews
                .Select(x => new AddNewsViewModel()
                {
                    Title = x.Title,
                    Source = x.Source,
                    LnkedForum = new ForumViewModel()
                    {
                        Topic = x.Forum.Topic
                    },
                    CommentsFromForum = x.Forum.Comments.Select(y => new CommentViewModel
                    {
                        Message = y.Message,
                        Id = y.Id
                    }).ToList()
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddNewsViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.CreaterId);

            var news = new News()
            {
                Title = viewModel.Title,
                Source = viewModel.Source,
                Creater = user
            };

            _newsRepository.Save(news);

            return RedirectToAction("All", "News");
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Remove(AddNewsViewModel addNewsViewModel)
        {

            var news = _newsRepository.Get(addNewsViewModel.Id);

            _newsRepository.Remove(news);

            return RedirectToAction("All", "News");
        }

        public IActionResult EasyRemove(long id)
        {
            var news = _newsRepository.Get(id);

            _newsRepository.Remove(news);

            return RedirectToAction("All", "News");
        }
    }
}

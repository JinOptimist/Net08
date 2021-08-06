using AutoMapper;
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
        private IMapper _mapper;

        public NewsController(NewsRepository newsRepository,
            UserRepository userRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AllInformation()
        {
            var allNews = _newsRepository.GetAll().Where(x => x.Forum != null && x.Forum.Comments != null).ToList();
            var viewModels = _mapper.Map<List<AllIformationViewModle>>(allNews);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AllNews()
        {
            var allNews = _newsRepository.GetAll();
            var viewModels = _mapper.Map<List<AllNewsViewModel>>(allNews);
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

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
    public class NewsController: Controller
    {
        private NewsRepository _newsRepository;

        public NewsController(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            var allUsers = _newsRepository.GetAll();

            var viewModels = allUsers
                .Select(x => new AddNewsViewModel()
                {
                     Title= x.Title,
                     Source= x.Source
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
            var news = new News()
            {
                Title = viewModel.Title,
                Source = viewModel.Source
            };

            _newsRepository.Save(news);

            return RedirectToAction("All", "News");
        }

        public IActionResult Remove(AddNewsViewModel addNewsViewModel)
        {

            var news = _newsRepository.Get(addNewsViewModel.Id);

            _newsRepository.Remove(news);

            return RedirectToAction("All", "News");
        }
    }
}

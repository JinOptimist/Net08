using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class NewsController: Controller
    {
        private MazeDbContext _mazeDbContext;

        public NewsController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;
        }

        [HttpGet]
        public IActionResult AllNews()
        {
            var allUsers = _mazeDbContext.News.ToList();

            var viewModels = allUsers
                .Select(x => new AddNewsViewModel()
                {
                     Title= x.Title,
                     Source= x.Source
                }).ToList();
            return View(viewModels);
        }

        [HttpPost]
        public IActionResult AddNews(AddNewsViewModel viewModel)
        {
            var news = new News()
            {
                Title = viewModel.Title,
                Source = viewModel.Source
            };

            _mazeDbContext.News.Add(news);

            _mazeDbContext.SaveChanges();

            return RedirectToAction("All", "News");
        }

        public IActionResult Remove(AddNewsViewModel addNewsViewModel)
        {

            var news = _mazeDbContext
                .News
                .Single(x => x.Id == addNewsViewModel.Id);

            _mazeDbContext.News.Remove(news);
            _mazeDbContext.SaveChanges();

            return RedirectToAction("All", "News");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository _newsRepository;
        private UserRepository _userRepository;
        private FileService _fileService;
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
            var news = _mapper.Map<News>(viewModel);
            news.Creater = _userRepository.Get(viewModel.CreaterId);
            _newsRepository.Save(news);

            var path = _fileService.GetPath(news.Id, "news");
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                viewModel.File.CopyTo(fileStream);
            }

            news.Url = _fileService.GetCatUrl(news.Id);
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

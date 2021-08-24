using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private CatRepository _catRepository;
        private FileService _fileService;
        private UserService _userService;
        private IMapper _mapper;

        public NewsController(NewsRepository newsRepository,
            UserRepository userRepository, IMapper mapper,
            FileService fileService, UserService userService,
            CatRepository catRepository)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
            _catRepository = catRepository;
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

            var path = _fileService.GetNewsPath(news.Id);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                viewModel.NewsFile.CopyTo(fileStream);
            }

            news.Url = _fileService.GetNewsUrl(news.Id);
            _newsRepository.Save(news);

            return RedirectToAction("AllNews", "News");
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

        public IActionResult DownloadTodayNews()
        {
            var pathToFile = _fileService.GetTempDocxFilePath();

            var user = _userService.GetCurrent();
            using (var file = DocX.Create(pathToFile))
            {
                foreach (var news in user.NewsCreatedByMe)
                {
                    file.InsertParagraph(news.Title);
                    var p = file.InsertParagraph($"With {news.Comments.Count()} comments");
                    p.Color(System.Drawing.Color.Red);
                    file.InsertParagraph("----------------");
                }

                file.InsertParagraph("А ещё у нас есть куча котиков. Смотрите какие они милые");

                var catFolder = _fileService.GetCatFolderPath();
                foreach (var catFilePath in Directory.GetFiles(catFolder))
                {
                    var pic = file.AddImage(catFilePath, "image/png").CreatePicture();
                    pic.Width = 400;
                    pic.Height = 400;
                    file.InsertParagraph().InsertPicture(pic);
                }

                file.Save();
            }

            return PhysicalFile(pathToFile,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "FileForUserName.docx");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMazeMvc.Controllers.AuthAttribute;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class CatController : Controller
    {
        private IMapper _mapper;
        private UserService _userService;
        private CatRepository _catRepository;
        private FileService _fileService;

        public CatController(UserService userService,
            CatRepository catRepository, IMapper mapper,
            FileService fileService)
        {
            _userService = userService;
            _catRepository = catRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        [Authorize]
        public IActionResult Gallery()
        {
            var viewModels = _mapper.Map<List<CatViewModel>>(_catRepository.GetAll());
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CatViewModel catViewModel)
        {
            var cat = _mapper.Map<Cat>(catViewModel);
            cat.Creater = _userService.GetCurrent();
            _catRepository.Save(cat);

            var path = _fileService.GetCatPath(cat.Id);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                catViewModel.CatFile.CopyTo(fileStream);
            }

            cat.Url = _fileService.GetCatUrl(cat.Id);
            _catRepository.Save(cat);

            return RedirectToAction("Gallery");
        }

        public IActionResult Remove(long id)
        {
            _catRepository.Remove(id);

            System.IO.File.Delete(_fileService.GetCatPath(id));

            return RedirectToAction("Gallery");
        }

        public IActionResult IsUniq(string name)
        {
            Thread.Sleep(3000);
            var isUniq = !_catRepository.Exist(name);
            return Json(isUniq);
        }
    }
}

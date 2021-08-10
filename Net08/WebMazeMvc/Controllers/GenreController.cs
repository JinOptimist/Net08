using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.Models;
using WebMazeMvc.EfStuff.Model;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Services;
using AutoMapper;

namespace WebMazeMvc.Controllers
{
    public class GenreController : Controller
    {
        private GenreRepository _genreRepository;
        private UserService _userService;
        private IMapper _mapper;

        public GenreController(GenreRepository genreRepository,
            UserService userService, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddGenre()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddGenre(GenreViewModel genre)
        { 
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            var newgenre = _mapper.Map<Genre>(genre);

            _genreRepository.Save(newgenre);

            return RedirectToAction("GenreAction");
        }
        //[HttpGet]
        //public IActionResult RemoveGenre()
        //{
        //    var viewModels = _mapper.Map<List<GenreViewModel>>(_genreRepository.GetAll());

        //    return View(viewModels);
        //}
        [HttpGet]
        public IActionResult RemoveGenre(long id)
        {
            var genreToRemove = _genreRepository.Get(id);

            if (genreToRemove == null)
            {
                return View();
            }

            _genreRepository.Remove(genreToRemove);

            return RedirectToAction("GenreAction");
        }
        public IActionResult GenreAction()
        {
            return View();
        }
        public IActionResult All()
        {
            var viewModel = _genreRepository.GetAll()
            .Select(x => new AllGenreGameViewModel
            {
                GenreName = x.GenreName,
                Id = x.Id,
                GenreGameViewModel = x.Games
                .Select(q => new GenreGameViewModel
                {
                    GameName = q.GameName,
                    Id = q.Id
                }).ToList()
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ChooseGenres()
        {
            var genres = _genreRepository.GetAll();

            var userGenresId = _userService.GetCurrent().FavoriteGenres.Select(x => x.Id).ToList();

            var viewmModel = _genreRepository.GetAll().Select(x => new GenreSelectedViewModel
            {
                GenreName = x.GenreName,
                Id = x.Id,
                IsSelected = userGenresId.Contains(x.Id)
            }).ToList();

            return View(viewmModel);
        }
    }
}

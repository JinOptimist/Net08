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

namespace WebMazeMvc.Controllers
{
    public class GenreController : Controller
    {

        private GenreRepository _genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult AddGenre()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddGenre(GenreViewModel genre)
        {
            
            var newgenre = new Genre
            {
                GenreName = genre.NameGenre
            };

            _genreRepository.Save(newgenre);
            return RedirectToAction("GenreAction");
        }
        public IActionResult RemoveGenre()
        {
            var allGenre = _genreRepository.GetAll();

            var viewModels = allGenre
               .Select(x => new GenreViewModel()
               {
                   Id = x.Id,
                   NameGenre = x.GenreName
               }).ToList();

            return View(viewModels);
        }
       
        public IActionResult DeleteGenre(int id)
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
            var allGenre = _genreRepository.GetAll();

            var viewModels = allGenre
               .Select(x => new AllGenreGameViewModel 
               {
                   NameGenre = x.GenreName,
                   Id = x.Id,
                   genreGameViewModel = x.Games
                   .Select(q => new GenreGameViewModel
                   {
                       NameGame = q.GameName,
                       Id = q.Id
                   }).ToList()
               }).ToList();

            return View(viewModels);
        }     
    }
}

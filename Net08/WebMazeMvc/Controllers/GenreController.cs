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

namespace WebMazeMvc.Controllers
{
    public class GenreController : Controller
    {
        private MazeDbContext _mazeDbContext;

        public GenreController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;
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
                GenreGame = genre.NameGenre
            };

            _mazeDbContext.Genres.Add(newgenre);
            _mazeDbContext.SaveChanges();
            return RedirectToAction("GenreAction");
        }
        public IActionResult RemoveGenre()
        {
            var allGenre = _mazeDbContext.Genres.ToList();

            var viewModels = allGenre
               .Select(x => new GenreViewModel()
               {
                   Id= x.Id,
                   NameGenre = x.GenreGame
               }).ToList();

            return View(viewModels);
        }
       
        public IActionResult DeleteGenre(int id)
        {
            var genreToRemove = _mazeDbContext.Genres.SingleOrDefault(x => x.Id == id);
            if (genreToRemove == null)
            {
                return View();
            }

            _mazeDbContext.Genres.Remove(genreToRemove);
            _mazeDbContext.SaveChanges();
            return RedirectToAction("GenreAction");
        }
        public IActionResult GenreAction()
        {
            return View();
        }
        public IActionResult All()
        {
            var allGenre = _mazeDbContext.Genres.ToList();

            var viewModels = allGenre
               .Select(x => new GenreViewModel()
               {
                  NameGenre =  x.GenreGame
               }).ToList();

            return View(viewModels);
        }
    }
}

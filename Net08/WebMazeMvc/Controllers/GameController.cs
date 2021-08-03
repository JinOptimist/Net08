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
    public class GameController : Controller
    {
        private GamesRepository _gamesRepository;
        private GenreRepository _genreRepository;

        public GameController(GamesRepository gamesRepository,
            GenreRepository genreRepository)
        {
            _gamesRepository = gamesRepository;
            _genreRepository = genreRepository;
        }

 
        public IActionResult AllGames()
        {
           var games =  _gamesRepository.GetAll();
            var viewModel = games.Select(x => new GameViewModel
            {
                NameGame = x.GameName,
                Link = x.Link,
                Url = x.Url
            }
                ).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            var genre = _genreRepository.GetAll();

            GameViewModel gameViewModel = new GameViewModel();

            gameViewModel.Genre = genre.Select(vb => new GenreSelected
            {
                GenreName = vb.GenreName,
                IsSelected = false,
                Id = vb.Id

            }).ToList();
            return View(gameViewModel);
        }

        [HttpPost]
        public IActionResult AddGame(GameViewModel newgame)
        {
            var result = new List<Genre>();

            var inputGenre = newgame.Genre.Where(x => x.IsSelected == true).ToList();

            var genres = _genreRepository.GetAll();

            for (int i = 0; i < inputGenre.Count(); i++)
            {
                var genre = genres.First(x => x.Id == inputGenre[i].Id);
                result.Add(genre);
            }
            var addgame = new Game()
            {
                GameName = newgame.NameGame,
                Link = newgame.Link,
                Url = newgame.Url,
                Genres = result
            };
            _gamesRepository.Save(addgame);

            return RedirectToAction("AllGames");
        }
    }
}

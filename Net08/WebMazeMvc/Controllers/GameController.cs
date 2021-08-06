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
            var games = _gamesRepository.GetAll();
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

            var gameViewModel = new GameViewModel();

            gameViewModel.Genre = genre.Select(vb => new GenreSelectedViewModel
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
            var ids = newgame.Genre.
                Where(x => x.IsSelected).
                Select(x => x.Id).
                ToList();

            var genres = _genreRepository.
                GetAll().
                Where(x => ids.Contains(x.Id)).
                ToList();

            var addgame = new Game()
            {
                GameName = newgame.NameGame,
                Link = newgame.Link,
                Url = newgame.Url,
                Genres = genres
            };
            _gamesRepository.Save(addgame);

            return RedirectToAction("AllGames");
        }
    }
}

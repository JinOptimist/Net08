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

        public GameController(GamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        public IActionResult AllGames()
        {
            var allgame = _gamesRepository.GetAll();

            var viewModels = allgame.Select(x => new GameViewModel()
            {
                Id = x.Id,
                GameName = x.GameName,
                Link = x.Link,
                Url = x.Url
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(GameViewModel newgame)
        {
            var game = new Game
            {
                GameName = newgame.GameName,
                Url = newgame.Url,
                Link = newgame.Link
            };

            _gamesRepository.Save(game);

            return RedirectToAction("AllGames");
        }

        public IActionResult Remove(long id)
        {
            var game = _gamesRepository.Get(id);

            _gamesRepository.Remove(game);

            return RedirectToAction("AllGames");
        }


        public IActionResult GamePage(long id)
        {
            var game = _gamesRepository.Get(id);

            var viewModels = new GameViewModel
            {
                Id = game.Id,
                GameName = game.GameName,
                Url = game.Url,
                Link = game.Link
            };

            return View(viewModels);
        }
    }
}

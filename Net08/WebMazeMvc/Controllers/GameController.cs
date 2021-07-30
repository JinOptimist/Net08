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

        private static List<GameViewModel> Games = new List<GameViewModel>() {

            new GameViewModel()
            {
                NameGame = "WoT",
                Link = "https://worldoftanks.ru/",
                Url = "data:image/png;base64,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"
            },

            new GameViewModel()
            {
                NameGame = "CS:GO",
                Link ="https://www.cybersport.ru/counter-strike-go",
                Url = "https://w7.pngwing.com/pngs/25/642/png-transparent-counter-strike-global-offensive-counter-strike-source-dota-2-logo-others-emblem-text-orange.png"
            }

        };

        public IActionResult AllGames()
        {
            var viewModel = Games;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(GameViewModel newgame)
        {
            var addgame = new Game()
            {
                GameName = newgame.NameGame,
                Link = newgame.Link,
                Url = newgame.Url
            };
            Games.Add(newgame);
            _gamesRepository.Save(addgame);
            return RedirectToAction("AllGames");
        }
    }
}

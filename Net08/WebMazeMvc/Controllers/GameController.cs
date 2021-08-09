using AutoMapper;
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
        private IMapper _mapper;

        public GameController(GamesRepository gamesRepository,
            GenreRepository genreRepository,
            IMapper mapper)
        {
            _gamesRepository = gamesRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
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

            var viewModel = _mapper.Map<GameViewModel>(genre);

            //var viewModel = new GameViewModel();

            //viewModel.Genres = genre.Select(vb => new GenreSelectedViewModel
            //{
            //    GenreName = vb.GenreName,
            //    IsSelected = false,
            //    Id = vb.Id

            //}).ToList();
            //var viewModel = _mapper.Map<GameViewModel>(_gamesRepository.GetAll()); 

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddGame(GameViewModel newgame)
        {
            var ids = newgame.Genres
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            var genres = _genreRepository
                .GetAll()
                .Where(x => ids.Contains(x.Id))
                .ToList();

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

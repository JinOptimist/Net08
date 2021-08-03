using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class BankCardController : Controller
    {
        private readonly MazeDbContext _mazeDbContext;

        public BankCardController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;  
        }
        
        [HttpGet]
        public IActionResult BankCardGetOne(int id)
        {
            var card = _mazeDbContext
                .BankCards
                .Where(x => x.Id == id)
                .SingleOrDefault();
            
            BankCardGetOneViewModel viewModel;

            if (card != null)
            {
                viewModel = new BankCardGetOneViewModel()
                {
                    Id = card.Id,
                    CardNumber = card.CardNumber,
                    ValidityMonth = card.ValidityMonth,
                    ValidityYear = card.ValidityYear
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }           
        }

        [HttpGet]
        public IActionResult BankCardGetAll()
        {
            var allCards = _mazeDbContext.BankCards.ToList();

            var viewModels = allCards
                .Select(x => new BankCardGetAllViewModel()
                {
                    Id = x.Id,
                    CardNumber = x.CardNumber,
                    ValidityMonth = x.ValidityMonth,
                    ValidityYear = x.ValidityYear
                }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult BankCardAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardAdd(BankCardAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BankCardAdd", viewModel);
            }

            var newCard = new BankCard()
            {
                CardNumber = viewModel.CardNumber,
                ValidityMonth = viewModel.ValidityMonth,
                ValidityYear = viewModel.ValidityYear
            };

            _mazeDbContext.Add(newCard);
            _mazeDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult BankCardRemove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardRemove(int id)
        {
            var card = _mazeDbContext
                .BankCards
                .SingleOrDefault(x => x.Id == id);

            if (card != null)
            {
                _mazeDbContext.BankCards.Remove(card);
                _mazeDbContext.SaveChanges();
            }

            return RedirectToAction("BankCardGetAll", "BankCard");
        }        
    }
}

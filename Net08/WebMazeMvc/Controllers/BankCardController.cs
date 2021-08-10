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
    public class BankCardController : Controller
    {
        private readonly BankCardRepository _bankCardRepository;
        private readonly IMapper _mapper;

        public BankCardController(BankCardRepository bankCardRepository, IMapper mapper)
        {
            _bankCardRepository = bankCardRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult BankCardGet(long id)
        {
            var card = _bankCardRepository.Get(id);

            if (card is null)
            {
                throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            }

            var viewModel = _mapper.Map<BankCardGetViewModel>(card);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult BankCardAll()
        {
            var allCards = _bankCardRepository.GetAll();
            var viewModels = _mapper.Map<List<BankCardGetViewModel>>(allCards);

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
                return View(viewModel);
            }

            var newCard = _mapper.Map<BankCard>(viewModel);
            _bankCardRepository.Save(newCard);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult BankCardRemove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardRemove(long id)
        {
            var card = _bankCardRepository.Get(id);

            if (card is null)
            {
                throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            }

            _bankCardRepository.Remove(card);

            return RedirectToAction("BankCardAll");
        }
    }
}
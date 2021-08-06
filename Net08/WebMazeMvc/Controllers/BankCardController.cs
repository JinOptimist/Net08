﻿using Microsoft.AspNetCore.Mvc;
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

        public BankCardController(BankCardRepository bankCardRepository)
        {
            _bankCardRepository = bankCardRepository;
        }

        [HttpGet]
        public IActionResult BankCardGetOne(long id)
        {
            var card = _bankCardRepository.Get(id);

            if (card == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new BankCardGetOneViewModel()
            {
                Id = card.Id,
                CardNumber = card.CardNumber,
                ValidityMonth = card.ValidityMonth,
                ValidityYear = card.ValidityYear
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult BankCardGetAll()
        {
            var allCards = _bankCardRepository.GetAll();

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
                return View(viewModel);
            }

            var newCard = new BankCard()
            {
                CardNumber = viewModel.CardNumber,
                ValidityMonth = viewModel.ValidityMonth,
                ValidityYear = viewModel.ValidityYear
            };

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

            if (card != null)
            {
                _bankCardRepository.Remove(card);
            }

            return RedirectToAction("BankCardGetAll", "BankCard");
        }

        public IActionResult CheckCardNumber(string cardNumber)
        {
            var a = _bankCardRepository.GetAll();          

            foreach (var item in a)
            {
                if (item.CardNumber == cardNumber)
                {
                    return Json(false);
                }
            }
              
            return Json(false);
        }
    }
}

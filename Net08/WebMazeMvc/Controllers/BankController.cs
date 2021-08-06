using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class BankController: Controller
    {
        

        private BankRepository _BankRepository;

        private IMapper _mapper;



        public BankController(BankRepository bankRepository, IMapper mapper)
        {
            _BankRepository = bankRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BanksAdding()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BanksAdding(BanksAddingViewModel viewModel)
        {
            var bank = _mapper.Map<Bank>(viewModel); 

            _BankRepository.Save(bank);

          

            return RedirectToAction("AllBanks");
        }
        [Authorize]
        public IActionResult AllBanks()
        {
            var allBanks = _BankRepository.GetAll();
            
            var viewModels = _mapper.Map<List<AllBanksForRemoveViewModel>>(allBanks); 

            return View(viewModels);
        }
        [Authorize]
        public IActionResult Remove(long id)
        {
            var bank = _BankRepository.Get(id);

            _BankRepository.Remove(bank);

            return RedirectToAction("AllBanks");
        }
    }
}

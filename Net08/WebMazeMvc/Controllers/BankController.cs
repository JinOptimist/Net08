using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        private UserRepository _userRepository;

        private IMapper _mapper;

        private FileService _fileService;
        public BankController(BankRepository bankRepository, IMapper mapper, FileService fileService, UserRepository userRepository)
        {
            _BankRepository = bankRepository;
            _mapper = mapper;
            _fileService = fileService;
            _userRepository = userRepository;
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
            var path = _fileService.GetBankPath(bank.Id);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                viewModel.BankLogoFile.CopyTo(fileStream);
            }

            bank.Url = _fileService.GetBankUrl(bank.Id);
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
        public IActionResult AllClientsOfBank(long IdBank)
        {
            var allUsers = _BankRepository.GetAllClients(IdBank);

            var viewModels = _mapper.Map<ClientOfBankViewModel>(allUsers);

            viewModels.IdBank = IdBank;

            return View(viewModels);
        }

        [Authorize]
        public IActionResult NewClientOfBank(long IdBank)
        {
            var viewModel = new NewClientOfBank();
            viewModel.IdBank = IdBank;
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewClientOfBankAdding(long IdBank)
        {
            var user = new User();
            user.Login = IdBank.Login;
            var Bank = _BankRepository.Get(IdBank);
            user.MyBanks.Add(Bank);
            _userRepository.Save(user);
            return RedirectToAction("AllClientsOfBank", IdBank.IdBank);
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

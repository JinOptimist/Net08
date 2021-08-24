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

        private IMapper _mapper;

        private FileService _fileService;
        public BankController(BankRepository bankRepository, IMapper mapper, FileService fileService)
        {
            _BankRepository = bankRepository;
            _mapper = mapper;
            _fileService = fileService;
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
        public IActionResult AllClientsOfBank(long id)
        {

            var allUsers = _BankRepository.GetAllClients(id);


             var viewModels = _mapper.Map<List<ClientOfBankViewModel>>(allUsers);

            return View(viewModels);
        }

        [Authorize]
        public IActionResult NewClient(long id)
        {

            //var User= _BankRepository.GetAllClients(id);


            //var viewModels = _mapper.Map<List<ClientOfBankViewModel>>(allUsers);
            return default;// View(viewModels);
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

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
    public class BankController: Controller
    {
        private MazeDbContext _mazeDbContext;

        public BankController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;
        }

        [HttpGet]
        public IActionResult BanksAdding()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BanksAdding(BanksAddingViewModel viewModel)
        {
            var bank = new Bank()
            {
                Name = viewModel.Name,
                Country = viewModel.Country
            };

            _mazeDbContext.Banks.Add(bank);

            _mazeDbContext.SaveChanges();

            return RedirectToAction("AllBanks");
        }

        public IActionResult AllBanks()
        {
            var allBanks = _mazeDbContext.Banks.ToList();

            var viewModels = allBanks
                .Select(x => new AllBanksForRemoveViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return View(viewModels);
        }

        public IActionResult Remove(long id)
        {
            var bank = _mazeDbContext
                .Banks
                .Single(x => x.Id == id);

            _mazeDbContext.Banks.Remove(bank);
            _mazeDbContext.SaveChanges();

            return RedirectToAction("AllBanks");
        }
    }
}

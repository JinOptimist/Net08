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
    public class UserController : Controller
    {
        private MazeDbContext _mazeDbContext;

        public UserController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel viewModel)
        {
            
            var user = new User()
            {
                Login = viewModel.Login,
                Password = viewModel.Password
            };

            _mazeDbContext.Users.Add(user);

            _mazeDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var allUsers = _mazeDbContext.Users.ToList();

            var viewModels = allUsers
                .Select(x => new UserForRemoveViewModel()
                {
                    Id = x.Id,
                    Login = x.Login
                }).ToList();

            return View(viewModels);
        }

        public IActionResult Remove(long id)
        {
            var user = _mazeDbContext
                .Users
                .Single(x => x.Id == id);

            _mazeDbContext.Users.Remove(user);
            _mazeDbContext.SaveChanges();

            return RedirectToAction("All");
        }
    }
}

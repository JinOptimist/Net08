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
    public class UserController : Controller
    {
        private UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
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

            _userRepository.Save(user);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var allUsers = _userRepository.GetAll();

            var viewModels = allUsers
                .Select(dbUser => new UserForRemoveViewModel()
                {
                    Id = dbUser.Id,
                    Login = dbUser.Login,
                    MyNews = dbUser.NewsCreatedByMe
                        .Select(x => new ShortNewsViewModel
                        {
                            Id = x.Id,
                            Title = x.Title
                        }).ToList()
                }).ToList();

            return View(viewModels);
        }

        public IActionResult Remove(long id)
        {
            var user = _userRepository.Get(id);

            _userRepository.Remove(user);

            return RedirectToAction("All");
        }
    }
}

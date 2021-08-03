using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _userRepository;
        private UserService _userService;

        public UserController(UserRepository userRepository, 
            UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.Login, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.Login),
                    "Не правильный логин или пароль");
                return View(viewModel);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Login));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthName));

            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthName);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
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

        public IActionResult MyNews()
        {
            var user = _userService.GetCurrent();

            var viewModels = user.NewsCreatedByMe
                .Select(x => new ShortNewsViewModel
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList();

            return View(viewModels);
        }

        public IActionResult Remove(long id)
        {
            var user = _userRepository.Get(id);

            _userRepository.Remove(user);

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult GetMyForums()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetForumsCreatedByMe()
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
    }
}

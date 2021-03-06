using AutoMapper;
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
        private GenreRepository _genreRepository;
        private IMapper _mapper;

        public UserController(UserRepository userRepository,
            UserService userService,
            GenreRepository genreRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var returnUrl = Request.Query["ReturnUrl"].FirstOrDefault();
            var viewModel = new RegistrationViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
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

            if (string.IsNullOrEmpty(viewModel.ReturnUrl))
            {
                return Redirect("/");
            }

            return Redirect(viewModel.ReturnUrl);
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
            var user = _mapper.Map<User>(viewModel);

            _userRepository.Save(user);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var allUsers = _userRepository.GetAll();

            var viewModels = _mapper.Map<List<UserForRemoveViewModel>>(allUsers);

            return View(viewModels);
        }

        public IActionResult MyNews()
        {
            var user = _userService.GetCurrent();

            var viewModels = _mapper.Map<List<ShortNewsViewModel>>(user.NewsCreatedByMe);

            return View(viewModels);
        }

        public IActionResult Remove(long id)
        {
            var user = _userRepository.Get(id);

            _userRepository.Remove(user);

            return RedirectToAction("All");
        }
        [HttpPost]
        public IActionResult FavoriteGenres(List<GenreSelectedViewModel> genreSelected)
        {
            var user = _userService.GetCurrent();

            var ids = genreSelected
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            user.FavoriteGenres.RemoveRange(0, user.FavoriteGenres.Count());

            user.FavoriteGenres = _genreRepository.FindGenresById(ids);

            _userRepository.Save(user);

            return RedirectToAction("FavoriteGenres");
        }
        [HttpGet]
        public IActionResult FavoriteGenres()
        {
            var user = _userService.GetCurrent();

            var viewModel = new UserGenresViewModel
            {
                Login = user.Login,
                FavoriteGenres = user.FavoriteGenres.Select(x => new GenreViewModel
                {
                    GenreName = x.GenreName,
                    Id = x.Id
                }).ToList()
            };

            return View(viewModel);
        }
    
        public IActionResult Denied(string returnUrl)
        {
            var user = _userService.GetCurrent();
            var viewModel = new DeniedViewModel()
            {
                DeniedUrl = returnUrl,
                RequestTime = DateTime.Now,
                UserName = user?.Login ?? "Guest"
            };
            return View(viewModel);
        }
    }
}

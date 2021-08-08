using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Controllers.AuthAttribute;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class CatController : Controller
    {
        private static List<CatViewModel> Girls = new List<CatViewModel>() {
            new CatViewModel()
                {
                    Name= "Kity",
                    Url = "https://i.pinimg.com/736x/75/1d/4b/751d4bda81598c27a15ac46874b3a305.jpg"
                },
                new CatViewModel()
                {
                    Name= "Kity 2",
                    Url = "https://i.pinimg.com/originals/2c/86/5d/2c865d628ff955fbc87e1ab106236dab.jpg"
                },
                new CatViewModel()
                {
                    Name= "Kity 3",
                    Url = "data:image/jpeg;base64,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"
                }
        };

        private UserService _userService;

        public CatController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var second = DateTime.Now.Second;
            var viewModel = new CatViewModel()
            {
                Second = second,
                Name = "Jo",
                Url = "asdf"
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Gallery()
        {
            var viewModel = Girls;

            return View(viewModel);
        }

        [HttpGet]
        [OnlyGirl]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [OnlyGirl]
        public IActionResult Add(CatViewModel cat)
        {
            Girls.Add(cat);
            return RedirectToAction("Gallery");
        }

        public IActionResult Remove(string name)
        {
            var girl = Girls.SingleOrDefault(x => x.Name == name);
            Girls.Remove(girl);
            return RedirectToAction("Gallery");
        }
    }
}

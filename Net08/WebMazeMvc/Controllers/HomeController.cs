using MazeCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;
using System.Threading;
using WebMazeMvc.Controllers.AuthAttribute;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class HomeController : Controller
    {
        private UserRepository _userRepository;

        public HomeController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.AllUsersOptions =
                _userRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Login,
                    Value = x.Id.ToString()
                })
                .ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(long coolUserId)
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

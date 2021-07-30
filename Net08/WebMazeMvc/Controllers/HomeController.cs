using MazeCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Repositories;

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
            var allUsers = _userRepository.GetAll();
            return View(allUsers.Count());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

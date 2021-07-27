using MazeCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMazeMvc.EfStuff;

namespace WebMazeMvc.Controllers
{
    public class HomeController : Controller
    {
        private MazeDbContext _mazeDbContext;

        public HomeController(MazeDbContext mazeDbContext)
        {
            _mazeDbContext = mazeDbContext;
        }

        public IActionResult Index()
        {
            var allUsers = _mazeDbContext.Users.ToList();
            return View(allUsers.Count());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

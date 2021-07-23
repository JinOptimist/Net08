using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models;

namespace WebMazeMvc.Controllers
{
    public class MazeController : Controller
    {
        [HttpGet]
        public IActionResult CreateMaze()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMaze(MazeViewModel mazeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(mazeViewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

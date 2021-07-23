using MazeCore;
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

        public IActionResult Draw()
        {
            var mazeBuilder = new MazeBuilder();
            var maze = mazeBuilder.Build(20, 20);

            var mazeViewModel = new MazeDrawViewModel()
            {
                Width = maze.Width,
                Height = maze.Height,
                Cells = new string[maze.Width, maze.Height]
            };

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    mazeViewModel.Cells[x, y] = maze[x, y].GetType().Name;
                }

            }

            return View(mazeViewModel);
        }
    }
}

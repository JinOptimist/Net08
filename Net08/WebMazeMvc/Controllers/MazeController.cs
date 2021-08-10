using MazeCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.Models;
using WebMazeMvc.EfStuff.Model;

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
        public IActionResult Draw(MazeViewModel mazeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateMaze", mazeViewModel);
            }

            var mazeBuilder = new MazeBuilder();
            var maze = mazeBuilder.Build(mazeViewModel.Width, mazeViewModel.Height);
            var mazeModle = new MazeModel()
            {
                Width = maze.Width,
                Height = maze.Height,
                Cells = maze.Cells,
            };

            var mazeDrawViewModel = new MazeDrawViewModel()
            {
                Width = maze.Width,
                Height = maze.Height,
                Cells = new string[maze.Width, maze.Height]
            };

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    mazeDrawViewModel.Cells[x, y] = maze[x, y].GetType().Name;
                }
            }

            return View(mazeDrawViewModel);
        }
    }
}

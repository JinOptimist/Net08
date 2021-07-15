using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MazeCore
{
    public class MazeBuilder
    {
        private IMaze _maze;
        private Random _random = new Random();
        private Action<IMaze> _drawStepByStep;

        public IMaze Build(int width = 10, int height = 5, Action<IMaze> drawStepByStep = null)
        {
            _drawStepByStep = drawStepByStep;
            _maze = new Maze()
            {
                Width = width,
                Height = height,
                Cells = new List<BaseCell>()
            };

            BuildWall();

            BuildGround();

            BuildGoldHeap(80);

            BuildTrap(20);

            return _maze;
        }

        private void BuildTrap(int chanceToCreateTrap)
        {           
            
            var allgolds = _maze.Cells.OfType<GoldHeap>().ToList();
            foreach (var gold in allgolds)
            {
                var numb = _random.Next(0, 10);
                for (int i = 0; i < chanceToCreateTrap / 10; i++)
                {
                    if (numb == i)
                    {
                        var ground = GetNears(gold).Last();
                        var trap = new Trap(ground.X, ground.Y, _maze, 3,40);
                        _maze.ReplaceCell(trap);
                        break;
                    }
                }

            }
        }

        private void BuildGoldHeap(int chanceToCreateGoldHeap)
        {
            var allgrounds = _maze.Cells.OfType<Ground>().ToList();
            foreach (var ground in allgrounds)
            {
                var numb = _random.Next(0, 10);
                if (GetNears(ground).OfType<Wall>().Count()==3)
                {
                    for (int i = 0; i < chanceToCreateGoldHeap / 10; i++)
                    {
                        if (numb == i)
                        {
                            var gold = new GoldHeap(ground.X, ground.Y, _maze, 500);
                            _maze.ReplaceCell(gold);
                            break;
                        }
                    }
                }
            }
        }

        private void BuildGround()
        {
            var wallsToDestroy = new List<BaseCell>() {
                GetRandom(_maze.Cells)
            };

            while (wallsToDestroy.Any())
            {
                if (_drawStepByStep != null)
                {
                    _drawStepByStep.Invoke(_maze);
                    Thread.Sleep(0);
                }

                var wallToDestroy = GetRandom(wallsToDestroy);

                var ground = new Ground(wallToDestroy.X, wallToDestroy.Y, _maze);
                var oldWall = _maze.ReplaceCell(ground);
                wallsToDestroy.Remove(oldWall);

                var nearestWalls = GetNears<Wall>(ground);
                wallsToDestroy.AddRange(nearestWalls);

                wallsToDestroy = wallsToDestroy
                    .Where(wall => GetNears<Ground>(wall).Count() < 2)
                    .ToList();
            }
        }

        private void BuildWall()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    var wall = new Wall(x, y, _maze);
                    _maze.Cells.Add(wall);
                }
            }
        }

        private IEnumerable<BaseCell> GetNears(BaseCell cell)
        {
            return GetNears<BaseCell>(cell);
        }

        private IEnumerable<BaseCell> GetNears<CellType>(BaseCell cell) 
            where CellType : BaseCell
        {
            return _maze.Cells
                .Where(c =>
                   Math.Abs(c.X - cell.X) == 0 && Math.Abs(c.Y - cell.Y) == 1
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 0
                )
                .OfType<CellType>();
        }

        private T GetRandom<T>(List<T> cells)
        {
            var index = _random.Next(cells.Count);
            return cells[index];
        }
    }
}

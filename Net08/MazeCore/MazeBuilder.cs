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
        private int _percentOfFood;

        public IMaze Build(int width = 10, int height = 5, int prercentOfFood = 10, Action<IMaze> drawStepByStep = null)
        {
            _drawStepByStep = drawStepByStep;
            _percentOfFood = prercentOfFood;

            _maze = new Maze()
            {
                Width = width,
                Height = height,
                Cells = new List<BaseCell>()
            };

            BuildWall();

            BuildGround();

            BuildFood();

            return _maze;
        }

        private void BuildFood()
        {
            if (_percentOfFood > 100  || _percentOfFood < 0)
            {
                throw new Exception("Can not be more than 100 percen and less than 0");
            }

            var CountOfFood = _maze.Cells
                .OfType<Ground>()
                .Count() * _percentOfFood / 100;

            var AllGround = _maze.Cells.OfType<Ground>().ToList();

            for (int i = 0; i < CountOfFood; i++)
            {
                var futurefood = GetRandom(AllGround);

                var food = new Food(futurefood.X, futurefood.Y, _maze, _random.Next(20));

                _maze.ReplaceCell(food);

                AllGround.Remove(futurefood);

                _drawStepByStep.Invoke(_maze);
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
                    Thread.Sleep(100);
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

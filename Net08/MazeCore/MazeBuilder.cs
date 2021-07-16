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

        public IMaze Build(int width = 10, 
            int height = 5, 
            Action<IMaze> drawStepByStep = null)
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

            BuildItemCell();

            return _maze;
        }

        private void BuildItemCell()
        {
            var grounds = _maze.Cells.OfType<Ground>().ToList();
            var rand = GetRandom(grounds);
            var bucket = new CellWithItem(rand.X, rand.Y, rand.Maze, ItemsConst.EmptyBucket);
            _maze.ReplaceCell(bucket);
        }

        private void BuildGround()
        {
            var randomCell = GetRandom(_maze.Cells);
            var wallsToDestroy = new List<BaseCell>() {
                randomCell
            };

            _maze.Hero.X = randomCell.X;
            _maze.Hero.Y = randomCell.Y; 

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

using MazeConsole.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeConsole
{
    public class MazeBuilder
    {
        private Maze _maze;
        private Random _random = new Random();
        public Maze Build(int width = 10, int height = 5)
        {
            _maze = new Maze()
            {
                Width = width,
                Height = height,
                Cells = new List<BaseCell>()
            };

            BuildWall();

            BuildGround();

            return _maze;
        }

        private void BuildGround()
        {
            int minerX = 0;
            int minerY = 0;
            var wallsToDestroy = new List<BaseCell>() {
                _maze.Cells.Single(x=> x.X == minerX && x.Y == minerY)
            };

            while (wallsToDestroy.Any())
            {
                var wallToDestroy = GetRandom(wallsToDestroy);
                minerX = wallToDestroy.X;
                minerY = wallToDestroy.Y;

                var ground = new Ground(minerX, minerY, _maze);
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

        private IEnumerable<BaseCell> GetNears<CellType>(BaseCell cell) where CellType : BaseCell
        {
            return _maze.Cells
                .Where(c =>
                   Math.Abs(c.X - cell.X) == 0 && Math.Abs(c.Y - cell.Y) == 1
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 0
                )
                .OfType<CellType>();
        }

        private BaseCell GetRandom(List<BaseCell> cells)
        {
            var index = _random.Next(cells.Count);
            return cells[index];
        }
    }
}

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
        private Maze _maze;
        private Random _random = new Random();
        private Action<Maze> _drawStepByStep;
        public Maze Build(int width = 10, int height = 10, Action<Maze> drawStepByStep = null)
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

            return _maze;
        }


        private void BuildGround()
        {
            var wallsToDestroy = new List<BaseCell>() {
                _maze.Cells[0]
            };
            var cellVisited = new List<BaseCell>();
            var allWallsToDestroy = wallsToDestroy;
            while (wallsToDestroy.Any()&& allWallsToDestroy.Any())
            {
                if (_drawStepByStep != null)
                {
                    _drawStepByStep.Invoke(_maze);
                    Thread.Sleep(100);
                }
                if (wallsToDestroy.Any())
                {
                    var wallToDestroy = GetRandom(wallsToDestroy);
                    allWallsToDestroy.Remove(wallToDestroy);

                    var ground = new Ground(wallToDestroy.X, wallToDestroy.Y, _maze);
                    var oldWall = _maze.ReplaceCell(ground);
                    wallsToDestroy.Remove(oldWall);

                    cellVisited.Add(ground);
                    wallsToDestroy = new List<BaseCell>();

                    var nearestWalls = GetNears<Wall>(ground);

                    wallsToDestroy.AddRange(nearestWalls);

                    wallsToDestroy = wallsToDestroy.Where(wall => GetNearsWithAStrictCondition<Ground>(wall).Count() < 3)
                        .ToList();
                    allWallsToDestroy.AddRange(wallsToDestroy);
                    while (!wallsToDestroy.Any()&& allWallsToDestroy.Any())
                    {
                        allWallsToDestroy = allWallsToDestroy.Where(wall => GetNearsWithAStrictCondition<Ground>(wall).Count() < 3)
                        .ToList();
                        var randomCellVisited = GetRandom(cellVisited);
                        var allNearestWalls = GetNearsWithAStrictCondition<Wall>(randomCellVisited);
                        var newWallToDestroy = allNearestWalls.Where(wall => GetNears<Ground>(wall).Count() < 2)
                        .ToList();
                        if (newWallToDestroy.Any())
                        {
                            wallsToDestroy.Add(GetRandom(newWallToDestroy));
                        }
                        else
                        {
                            cellVisited.Remove(randomCellVisited);
                        }                   
                    }
                }
            }
            var deadlock = _maze.Cells.Where(ground => GetNears<Wall>(ground).Count() >= 3).ToList();
            foreach (var cell in deadlock)
            {
                var ground = new Gold(cell.X, cell.Y, _maze);
                var oldWall = _maze.ReplaceCell(ground);
            }
        }
        //private void BuildGround()
        //{
        //var wallsToDestroy = new List<BaseCell>() {
        //        GetRandom(_maze.Cells)
        //    };

        //    while (wallsToDestroy.Any())
        //    {
        //        if (_drawStepByStep != null)
        //        {
        //            _drawStepByStep.Invoke(_maze);
        //            Thread.Sleep(100);
        //        }

        //        var wallToDestroy = GetRandom(wallsToDestroy);

        //        var ground = new Ground(wallToDestroy.X, wallToDestroy.Y, _maze);
        //        var oldWall = _maze.ReplaceCell(ground);
        //        wallsToDestroy.Remove(oldWall);

        //        var nearestWalls = GetNears<Wall>(ground);
        //        wallsToDestroy.AddRange(nearestWalls);

        //        wallsToDestroy = wallsToDestroy
        //            .Where(wall => GetNears<Ground>(wall).Count() < 2)
        //            .ToList();
        //    }
        //}
        
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
        private IEnumerable<BaseCell> GetNearsWithAStrictCondition<CellType>(BaseCell cell) where CellType : BaseCell
        {
            return _maze.Cells
                .Where(c =>
                   Math.Abs(c.X - cell.X) == 0 && Math.Abs(c.Y - cell.Y) == 1
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 0
                || Math.Abs(c.X - cell.X) == 1 && Math.Abs(c.Y - cell.Y) == 1
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

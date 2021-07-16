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

            BuildGroundMyVersion();

            BuildGoldHeap(20);

            return _maze;
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
        private void BuildGroundMyVersion()
        {
            var wallsToDestroy = new List<BaseCell>() {
                _maze.Cells[0]
            };

            var cellVisited = new List<BaseCell>();
            var allWallsToDestroy = wallsToDestroy;
            while (wallsToDestroy.Any() && allWallsToDestroy.Any())
            {
                if (_drawStepByStep != null)
                {
                    _drawStepByStep.Invoke(_maze);
                    Thread.Sleep(100);
                }
                if (wallsToDestroy.Any())
                {
                    var ToDestroy = GetRandom(wallsToDestroy);
                    allWallsToDestroy.Remove(ToDestroy);

                    var ground = new Ground(ToDestroy.X, ToDestroy.Y, _maze);
                    var oldWall = _maze.ReplaceCell(ground);
                    wallsToDestroy.Remove(oldWall);

                    cellVisited.Add(ground);
                    wallsToDestroy = new List<BaseCell>();

                    var nearestWalls = GetNears<Wall>(ground);


                    wallsToDestroy.AddRange(nearestWalls);


                    wallsToDestroy = wallsToDestroy
                        .Where(wall => GetNears<Ground>(wall).Count() < 2)
                        .ToList();
                    wallsToDestroy = wallsToDestroy.Where(wall => GetNearsWhithDiagonals<Ground>(wall).Count() < 3)
                        .ToList();
                    allWallsToDestroy.AddRange(wallsToDestroy);
                    while (!wallsToDestroy.Any() && allWallsToDestroy.Any())
                    {
                        allWallsToDestroy = allWallsToDestroy.Where(wall => GetNearsWhithDiagonals<Ground>(wall).Count() < 3)
                        .ToList();
                        var randomCellVisited = GetRandom(cellVisited);
                        var allNearestWalls = GetNearsWhithDiagonals<Wall>(randomCellVisited);
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
        }
        private void BuildGoldHeap(int gold)
        {
            var deadlock = _maze.Cells.Where(ground => GetNears<Wall>(ground).Count() >= 3).ToList();
            foreach (var cell in deadlock)
            {
                var ground = new GoldHeap(cell.X, cell.Y, _maze, gold);
                var oldwall = _maze.ReplaceCell(ground);
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
        private IEnumerable<BaseCell> GetNearsWhithDiagonals<CellType>(BaseCell cell)
           where CellType : BaseCell
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

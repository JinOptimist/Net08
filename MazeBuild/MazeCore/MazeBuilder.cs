
using MazeCore.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeCore
{
    public class MazeBuilder
    {
        private Maze _maze { get; set; }
        private Random _random = new Random();
        private Action<Maze> _drawStepByStep;
        public  Maze Build(int width,int height,Action<Maze>drawStepByStep = null)
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

            for (int i = 0; i < _random.Next(0,5); i++) 
            { 
                BuildPit(); 
            }

            return _maze;
        }

        public void BuildGround()
        {

            var wallsToDestroy = new List<BaseCell>() { GetRandom(_maze.Cells) };
           
            while (wallsToDestroy.Any())
            {
                if (_drawStepByStep != null)
                {
                    _drawStepByStep.Invoke(_maze);
                    Thread.Sleep(100);
                }
                

                var wallToDestroy = GetRandom(wallsToDestroy);
             
                var ground = new Ground(wallToDestroy.X, wallToDestroy.Y, _maze);
               
                var oldwall = _maze.ReplaceCells(ground);
                wallsToDestroy.Remove(oldwall);
                var near = GetNears<Wall>(ground);
                wallsToDestroy.AddRange(near);
                wallsToDestroy = wallsToDestroy.Where(x => GetNears<Ground>(x).Count() < 2).ToList();
                
            }
        }
        public void BuildPit()
        {
            var wall = GetRandom<Wall>(_maze.Cells.OfType<Wall>().ToList());
            var pit = new Pit(wall.X, wall.Y,_maze);
            _maze.ReplaceCells(pit);
        }
        public void BuildWall()
        {
            for(int y = 0; y < _maze.Height; y++)
            {
                for(int x =0;x< _maze.Width; x++)
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
        private IEnumerable<BaseCell> GetNears<CellType>(BaseCell cell) where CellType:BaseCell
        {
            return _maze.Cells.Where(x => (Math.Abs(x.X - cell.X) == 1 && Math.Abs(x.Y - cell.Y) == 0)
            || (Math.Abs(x.Y - cell.Y) == 1 && (Math.Abs(x.X - cell.X) == 0))).OfType<CellType>();
        }
        private T GetRandom<T>(List<T> cells)
        {
            
            var index = _random.Next(cells.Count);
            return cells[index];
        }
    }
}

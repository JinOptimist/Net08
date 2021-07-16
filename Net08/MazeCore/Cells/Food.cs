using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Food : BaseCell
    {
        private int _food;
        public Food(int x, int y, IMaze maze, int food) : base(x, y, maze)
        {
            _food = food;
            if (_food < 0 || _food > 100)
            {
                throw new Exception("Gold heap can't has negative gold count");
            }
        }

        public override bool TryStep()
        {
            Maze.Hero.Stamina += _food;
            var ground = new Ground(X, Y, Maze);
            Maze.ReplaceCell(ground);
            return true;
        }
    }
}

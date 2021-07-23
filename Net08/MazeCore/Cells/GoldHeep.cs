using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class GoldHeep : BaseCell
    {
        private int _goldCount { get; set; }

        public GoldHeep(int x, int y, Maze maze, int goldCount) : base(x, y, maze)
        {
            _goldCount = goldCount;
        }

        public override bool TryStep()
        {
            Maze.Hero.Gold += _goldCount;
            var ground = new Ground(X, Y, Maze);
            Maze.ReplaceCell(ground);
            return true;
        }
    }
}

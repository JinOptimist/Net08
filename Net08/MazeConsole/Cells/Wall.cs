using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.Cells
{
    public class Wall : BaseCell
    {
        public Wall(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep()
        {
            return false;
        }
    }
}

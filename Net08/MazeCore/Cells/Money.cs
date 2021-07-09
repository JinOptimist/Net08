using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Money : BaseCell
    {
        public Money(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}

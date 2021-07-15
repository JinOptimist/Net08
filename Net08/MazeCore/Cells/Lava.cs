using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Lava : BaseCell
    {
        public Lava(int x, int y, IMaze maze) : base(x, y, maze)
        {

        }

        public override bool TryStep()
        {

            return false;
        }
    }
}

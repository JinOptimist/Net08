using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.Cells
{
    public class Teleport : BaseCell
    {
        public Teleport(int x, int y, IMaze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep()
        {
            var item = Maze.GetCellRandomOthers<Teleport>(this);
            Maze.Hero.X = item.X;
            Maze.Hero.Y = item.Y;
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class CellWithItem : BaseCell
    {
        public string Item { get; set; }

        public CellWithItem(int x, int y, IMaze maze, string item) : base(x, y, maze)
        {
            Item = item;
        }

        public override bool TryStep()
        {
            Maze.Hero.Inventory.Add(Item);

            Maze.ReplaceCellToGround(this);

            return true;
        }
    }
}

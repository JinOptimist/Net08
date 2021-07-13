
using MazeCore.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCore
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; }
        public BaseCell ReplaceCells(BaseCell newCell)
        {
            var oldCell = Cells.Single(x => x.X == newCell.X && x.Y == newCell.Y);
            Cells.Remove(oldCell);
            Cells.Add(newCell);
            return oldCell;
        }
    }
}

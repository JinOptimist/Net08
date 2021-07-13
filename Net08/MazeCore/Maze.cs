using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; }
        public Hero Hero { get; set; }

        /// <summary>
        /// Remove old cell with the same coordinate and return add new cell
        /// </summary>
        /// <param name="newCell"></param>
        /// <returns>Removed cell</returns>
        public BaseCell ReplaceCell(BaseCell newCell)
        {
            var oldCell = Cells.Single(cell => cell.X == newCell.X && cell.Y == newCell.Y);
            Cells.Remove(oldCell);
            Cells.Add(newCell);
            return oldCell;
        }
    }
}

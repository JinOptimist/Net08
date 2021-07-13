
using MazeCore;
using MazeCore.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeBuild
{
   public class MazeDrawer
    {
        public void Draw(Maze maze)
        {
            foreach(var cell in maze.Cells)
            {
                Console.SetCursorPosition(cell.X, cell.Y);
                if(cell is Wall)
                {
                    Console.Write("#");
                }
                if(cell is Ground)
                {
                    Console.Write(".");
                }
                if (cell is Pit)
                {
                    Console.Write("o");
                }
            }
            var top = maze.Cells.Max(c => c.Y);
            Console.SetCursorPosition(0, top + 1);
        }
    }
}

using MazeCore;
using MazeCore.Cells;
using System;
using System.Text;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public void Draw(IMaze maze)
        {
            foreach (var cell in maze.Cells)
            {
                Console.SetCursorPosition(cell.X, cell.Y);
                if (cell is Wall)
                {
                    //Console.Write("U+2B1C",UTF32Encoding.Equals(U+2B1C));
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine("#");
                }
                if (cell is Ground)
                {
                    Console.Write(".");
                }
                if (cell is GoldHeap)
                {
                    Console.Write("$");
                }
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

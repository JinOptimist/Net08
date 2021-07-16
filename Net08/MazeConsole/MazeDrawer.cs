using MazeCore;
using MazeCore.Cells;
using System;

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
                    Console.Write("#");
                }
                else if (cell is Ground)
                {
                    Console.Write(".");
                }
                else if (cell is Teleport)
                {
                    Console.Write("T");
                }
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}
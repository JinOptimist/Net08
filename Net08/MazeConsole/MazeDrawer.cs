using MazeCore;
using MazeCore.Cells;
using System;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public void Draw(Maze maze)
        {
            foreach (var cell in maze.Cells)
            {
                Console.SetCursorPosition(cell.X, cell.Y);
                if (cell is Wall)
                {
                    Console.Write("#");
                }
                if (cell is Ground)
                {
                    Console.Write(".");
                }
                if (cell is Gold)
                {
                    Console.Write("&");
                }
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

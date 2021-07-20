using MazeCore;
using MazeCore.Cells;
using System;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public void Draw(IMaze maze)
        {
            foreach (var cell in maze.CellsWithHero)
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
                if (cell is CellWithItem)
                {
                    Console.Write("?");
                }
                if (cell is CellWithHero)
                {
                    Console.Write("@");
                }
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

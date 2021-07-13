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
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" "); //стены
                }
                if (cell is Ground)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" "); //проходы
                }
                /*if (cell is Water)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(" "); //вода
                }*/
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

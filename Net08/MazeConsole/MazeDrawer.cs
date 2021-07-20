using MazeCore;
using MazeCore.Cells;
using System;

namespace MazeConsole
{
    public class MazeDrawer
    {
        public void Draw(IMaze maze)
        {
            var defoltCollor = ConsoleColor.Black;

            foreach (var cell in maze.Cells)
            {
                Console.BackgroundColor = defoltCollor;

                Console.SetCursorPosition(cell.X, cell.Y);
                if (cell is Wall)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" "); //стены
                }
                if (cell is Ground)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" "); //проходы
                }
                if (cell is Lava)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("$"); //лава
                }
            }

            Console.BackgroundColor = defoltCollor;

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

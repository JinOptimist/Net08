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
                Console.BackgroundColor = ConsoleColor.Black;
                var defoltCollor = Console.BackgroundColor;

                Console.SetCursorPosition(cell.X, cell.Y);
                if (cell is Wall)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("#"); //стены
                    Console.BackgroundColor = defoltCollor;
                }
                if (cell is Ground)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" "); //проходы
                    Console.BackgroundColor = defoltCollor;
                }
                if (cell is Lava)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("+"); //лава
                    Console.BackgroundColor = defoltCollor;
                }
            }

            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}

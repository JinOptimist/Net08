using MazeCore;
using System;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();

            var drawer = new MazeDrawer();

            var maze = builder.Build(8, 8, drawer.Draw);

            drawer.Draw(maze);

            var exit = false;
            while (!exit)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        maze.TryToStep(Direction.Up);
                        break;
                    case ConsoleKey.RightArrow:
                        maze.TryToStep(Direction.Right);
                        break;
                    case ConsoleKey.DownArrow:
                        maze.TryToStep(Direction.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        maze.TryToStep(Direction.Left);
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }

                drawer.Draw(maze);
            }
        }
    }
}

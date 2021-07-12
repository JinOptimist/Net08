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

            var maze = builder.Build(50, 30, drawer.Draw);

            drawer.Draw(maze);

            Console.ReadLine();
        }
    }
}

using MazeCore;
using System;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dima was here
            var builder = new MazeBuilder();

            var drawer = new MazeDrawer();

            var maze = builder.Build(10, 10,5, drawer.Draw);

            drawer.Draw(maze);

            Console.ReadLine();
            
        }
    }
}

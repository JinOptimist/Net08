
using MazeCore;
using System;
using System.Linq;
using System.Threading;

namespace MazeBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();
            var drawer = new MazeDrawer();
            var maze = builder.Build(10,10,drawer.Draw);
            new MazeDrawer().Draw(maze);
           
        }
    }
}

using System;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new MazeBuilder();
            
            var maze = builder.Build();
        }
    }
}

using MazeCore;
using System;
using System.Reflection;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var mazeDrawer = new MazeDrawer();
            Type type = mazeDrawer.GetType();

            ShowTypeInfo(typeof(Maze));

            Console.ReadLine();
            //Maze();
        }

        public static void ShowTypeInfo(Type type)
        {
            Console.WriteLine(type.FullName);

            Console.WriteLine("------ All Properties");
            foreach (var propertyInfo in type.GetProperties())
            {
                Console.WriteLine($"{propertyInfo.PropertyType.FullName} {propertyInfo.Name}: ");
            }

            Console.WriteLine("------ All Methods");
            foreach (var methodInfo in type.GetMethods())
            {
                Console.WriteLine($"{methodInfo.ReturnType.FullName} {methodInfo.Name} {methodInfo.GetParameters().Length} ");
            }
        }

        public static void Maze()
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

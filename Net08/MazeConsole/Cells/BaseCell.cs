using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole
{
    public abstract class BaseCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Maze Maze { get; private set; }

        public BaseCell(int x, int y, Maze maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }

        public abstract bool TryStep();
    }
}

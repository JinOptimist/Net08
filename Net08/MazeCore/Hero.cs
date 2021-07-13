using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore
{
    public class Hero
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Maze Maze { get; set; }

        public int Gold { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore
{
    public class Hero
    {
        public int x { get; set; }
        public int y { get; set; }

        public Maze Maze { get; set; }
        public int Gold { get; set; }
    }
}

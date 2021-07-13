using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore
{
    public class Hero : IHero
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IMaze Maze { get; set; }
        public int Gold { get; set; }
        public int HP { get; set; }
        public int Stamina { get; set; }
    }
}

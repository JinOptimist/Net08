using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    class GoldHeap : BaseCell
    {
        private int _goldCount;
        public GoldHeap(int x, int y, Maze maze, int godCount) : base(x, y,  )  // что сюда&
        {
            _goldCount = godCount;
        }
        public override bool TryStep()
        {
            Maze.Hero.Gold += _goldCount;
            var ground = new Ground (X, Y, Maze);
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Water : BaseCell
    {
        private int _damage;
        public Water(int x, int y, IMaze maze, int damage) : base(x, y, maze)
        {
            _damage = damage;
        }

        public override bool TryStep()
        {
            Maze.Hero.HP -= _damage;
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.Cells
{
    class Guard : BaseCell
    {
        private int _lastX;
        private int _lastY;
        private int _damage;
        private int _health;

        public Guard(int x, int y, IMaze maze, int damage, int health) : base(x, y, maze)
        {
            _damage = damage;
            _health = health;
            _lastX = x;
            _lastY = y;
        }
        public Guard(Guard guard, int lastX, int lastY) : base(guard.X, guard.Y, guard.Maze)
        {
            _damage = guard._damage;
            _health = guard._health;
            _lastX = guard._lastX;
            _lastY = guard._lastY;
        }

        public override bool TryStep()
        {
            _health -= Maze.Hero.Damage;
            if (_health > 0)
            {
                Maze.Hero.HP -= _damage;
                return false;
            }
            else
            {
                var ground = new Ground(X, Y, Maze);
                Maze.ReplaceCell(ground);
                return true;
            }
        }

        public override void FinishStepHero()
        {
            var items = Maze.Cells
                .Where(c =>
                   (Math.Abs(c.X - X) == 0 && Math.Abs(c.Y - Y) == 1
                 || Math.Abs(c.X - X) == 1 && Math.Abs(c.Y - Y) == 0)
                 && (X != _lastX || Y != _lastY)
                )
                .OfType<Ground>()
                .ToList();

            if (items.Count > 0)
            {
                var moveCell = items[0];

                var ground = new Ground(X, Y, Maze);
                Maze.ReplaceCell(ground);

                var guard = new Guard(this, moveCell.X, moveCell.Y);
                Maze.ReplaceCell(guard);
            }

            _lastX = X;
            _lastY = Y;
        }
    }
}

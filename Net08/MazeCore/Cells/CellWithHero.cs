using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class CellWithHero : BaseCell
    {
        public CellWithHero(IHero hero, IMaze maze) : base(hero.X, hero.Y, maze)
        {
        }

        public override bool TryStep()
        {
            throw new NotImplementedException();
        }
    }
}

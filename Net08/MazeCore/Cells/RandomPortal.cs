using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore.Cells
{
    public class RandomPortal : BaseCell
    {
        public RandomPortal(int x, int y, IMaze maze) : base(x, y, maze)
        {            
        }

        public override bool TryStep()
        {
            var ground = new Ground(X, Y, Maze);
            Maze.ReplaceCell(ground);

            var listOfGrounds = Maze.Cells.OfType<Ground>().ToList();

            var random = new Random();
            int randomIndex = random.Next(listOfGrounds.Count - 1);

            Maze.Hero.Y = listOfGrounds[randomIndex].Y;
            Maze.Hero.X = listOfGrounds[randomIndex].X;
            
            return true;
        }
    }
}

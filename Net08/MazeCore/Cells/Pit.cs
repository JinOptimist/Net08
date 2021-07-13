using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Pit:BaseCell
    {     
        public Pit(int x, int y, Maze maze) : base(x, y, maze)
        {
           
        }    
        public override bool TryStep()
        {
            return true;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MazeCore.Cells
{
    public class Trap : BaseCell
    {
        public  int _trapCharges;
        private  int _hpLose;

        public Trap(int x, int y, IMaze maze,int trapCharges,int hpLose) : base(x, y, maze)
        {
            if ((hpLose < 0) || (trapCharges < 0))
                throw new Exception();
            _trapCharges = trapCharges;
            _hpLose = hpLose;
        }

        public override bool TryStep()
        {
            if (_trapCharges == 0)
            {
                var ground = new Ground(X, Y, Maze);
                Maze.ReplaceCell(ground);
            }
            else
            { 
                  Maze.Hero.HP -= _hpLose;
                if (Maze.Hero.HP < 0)
                    Maze.Hero.HP = 0;
                _trapCharges--;
            }           
            return true;
        }
    }
}

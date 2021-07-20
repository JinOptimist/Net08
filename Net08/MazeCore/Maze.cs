using MazeCore.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeCore
{
    public class Maze : IMaze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; }
        public List<BaseCell> CellsWithHero
        {
            get
            {
                var copyCells = Cells.ToList();
                var badCell = copyCells.Single(c => c.X == Hero.X && c.Y == Hero.Y);
                copyCells.Remove(badCell);
                copyCells.Add(new CellWithHero(Hero, this));
                return copyCells;
            }
        }
        public IHero Hero
        {
            get
            {
                return HeroSingleton.GetHero();
            }
        }

        public BaseCell this[int x, int y]
        {
            get
            {
                return Cells.SingleOrDefault(c => c.X == x && c.Y == y);
            }
            set
            {
                var oldCell = Cells.SingleOrDefault(c => c.X == x && c.Y == y);
                Cells.Remove(oldCell);
                Cells.Add(value);
            }
        }

        /// <summary>
        /// Remove old cell with the same coordinate and return add new cell
        /// </summary>
        /// <param name="newCell"></param>
        /// <returns>Removed cell</returns>
        public BaseCell ReplaceCell(BaseCell newCell)
        {
             var oldCell = Cells.Single(cell => cell.X == newCell.X && cell.Y == newCell.Y);
            Cells.Remove(oldCell);
            Cells.Add(newCell);
            return oldCell;
        }

        public BaseCell ReplaceCellToGround(BaseCell oldCell)
        {
            var ground = new Ground(oldCell.X, oldCell.Y, oldCell.Maze);
            ReplaceCell(ground);
            return ground;
        }

        public void TryToStep(Direction direction)
        {
            var destinationX = Hero.X;
            var destinationY = Hero.Y;
            switch (direction)

            {
                case Direction.Up:
                    destinationY--;
                    break;
                case Direction.Right:
                    destinationX++;
                    break;
                case Direction.Down:
                    destinationY++;
                    break;
                case Direction.Left:
                    destinationX--;
                    break;
                default:
                    throw new Exception("Unkown direction");
            }

            var cellDestination = this[destinationX, destinationY];
            if (cellDestination?.TryStep() ?? false)
            {
                Hero.X = destinationX;
                Hero.Y = destinationY;
            }
        }
    }
}

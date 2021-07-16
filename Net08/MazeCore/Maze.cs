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
        public IHero Hero { get; set; }

        private Random _random = new Random();

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

        /// <summary>
        /// Get other random cell of current type
        /// </summary>
        /// <typeparam name="CellType"></typeparam>
        /// <param name="cell"></param>
        /// <returns>Searched cell</returns>
        public BaseCell GetCellRandomOthers<CellType>(BaseCell cell) where CellType : BaseCell
        {
            return GetRandom(GetCellsOthers<Teleport>(cell).ToList());
        }

        private IEnumerable<BaseCell> GetCellsOthers<CellType>(BaseCell cell) where CellType : BaseCell
        {
            return Cells
                .Where(c =>
                   c.X != cell.X || c.Y != cell.Y
                )
                .OfType<CellType>();
        }

        public T GetRandom<T>(List<T> cells)
        {
            var index = _random.Next(cells.Count);
            return cells[index];
        }
    }
}

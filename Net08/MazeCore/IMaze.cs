using MazeCore.Cells;
using System.Collections.Generic;

namespace MazeCore
{
    public interface IMaze
    {
        List<BaseCell> Cells { get; set; }
        List<BaseCell> CellsWithHero { get; }
        IHero Hero { get; }
        int Height { get; set; }
        int Width { get; set; }
        BaseCell this[int x, int y] { get; set; }

        BaseCell ReplaceCell(BaseCell newCell);
        BaseCell ReplaceCellToGround(BaseCell oldCell);
        void TryToStep(Direction direction);
    }
}
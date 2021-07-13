using MazeCore.Cells;
using System.Collections.Generic;

namespace MazeCore
{
    public interface IMaze
    {
        List<BaseCell> Cells { get; set; }
        int Height { get; set; }
        IHero Hero { get; set; }
        int Width { get; set; }

        BaseCell ReplaceCell(BaseCell newCell);
    }
}
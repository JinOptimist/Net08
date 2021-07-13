

namespace MazeCore.Cell
{
    public class Pit : BaseCell
    {
        public Pit(int x, int y, Maze maze) : base(x, y, maze)
        {

        }
        public override bool TryStep()
        {
            return false;
        }
    }
}

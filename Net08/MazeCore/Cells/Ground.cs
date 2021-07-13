namespace MazeCore.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, Maze maze) : base(x, y, maze)
        {

        }

        public override bool TryStep()
        {
            return true;
        }
    }
}

namespace MazeCore.Cells
{
    public class GoldHeap : BaseCell
    {
        private int _goldCount;

        public GoldHeap(int x, int y, IMaze maze, int goldCount) : base(x, y, maze)
        {
            if (goldCount < 0)
            {
                throw new System.Exception("Gold heap");
            }
            _goldCount = goldCount;
        }

        public override bool TryStep()
        {
            Maze.Hero.Gold += _goldCount;
            var ground = new Ground(X, Y, Maze);
            Maze.ReplaceCell(ground);
            return true;
        }
    }
}

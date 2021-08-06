namespace MazeCore.Cells
{
    public abstract class BaseCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IMaze Maze { get; private set; }

        public BaseCell(int x, int y, IMaze maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }

        public abstract bool TryStep();
        public virtual void FinishStepHero() { }
    }
}

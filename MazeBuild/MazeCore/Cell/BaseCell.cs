

namespace MazeCore.Cell
{
    public abstract class BaseCell
    {
        public BaseCell(int x, int y,Maze maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }

    

        public int X { get; set; }
        public int Y { get; set; }
        public Maze Maze { get;private set; }

        public abstract bool TryStep();
        
     
    }
}

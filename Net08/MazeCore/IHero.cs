namespace MazeCore
{
    public interface IHero
    {
        int Gold { get; set; }
        Maze Maze { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
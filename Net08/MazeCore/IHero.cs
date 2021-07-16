namespace MazeCore
{
    public interface IHero
    {
        int Gold { get; set; }
        IMaze Maze { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int HP { get; set; }
        int Stamina { get; set; }
    }
}
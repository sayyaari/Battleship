namespace Battleship.Model
{
    public interface IBoardGrid
    {
        BoardDimension Dimension { get; init; }

        bool TryGet(Position position, out ICell? cell);
    }
}
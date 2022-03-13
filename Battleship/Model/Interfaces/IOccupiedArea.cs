namespace Battleship.Model.Interfaces
{
    public interface IOccupiedArea
    {
        bool HasSunkShip { get; }
        Ship Ship { get; init; }
        IEnumerable<ICell> Cells { get; init; }
    }
}
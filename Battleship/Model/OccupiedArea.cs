namespace Battleship.Model
{
    public class OccupiedArea
    {
        public OccupiedArea(Ship ship, Position startPosition, Direction direction, IEnumerable<ICell> cells)
        {
            Ship = ship;
            StartPosition = startPosition;
            Direction = direction;
            Cells = cells;
            Cells.ToList().ForEach(cell =>cell.TryOccupy(ship));
        }

        public Ship Ship { get; init; }

        public Position StartPosition { get; init; }

        public Direction  Direction { get; init; }

        public IEnumerable<ICell> Cells { get; init; }
    }
}

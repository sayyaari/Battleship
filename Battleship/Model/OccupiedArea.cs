namespace Battleship.Model
{
    public class OccupiedArea
    {
        public Ship Ship { get; init; }

        public Position StartPosition { get; init; }

        public Direction  Direction { get; init; }

        public IEnumerable<Cell> Cells { get; init; }
    }
}

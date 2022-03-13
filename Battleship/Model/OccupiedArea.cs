namespace Battleship.Model
{
    public class OccupiedArea
    {
        public OccupiedArea(Ship ship, IEnumerable<ICell> cells)
        {
            Ship = ship;
            Cells = cells;
            Cells.ToList().ForEach(cell => cell.TryOccupy(ship));
        }

        public Ship Ship { get; init; }
        public IEnumerable<ICell> Cells { get; init; }
        public bool HasSunkShip => Cells.All(cell => cell.HasHit);
    }
}

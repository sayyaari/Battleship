namespace Battleship.Model
{
    public class Cell
    {
        public Cell(Position position)
        {
            Position = position;
        }

        public Position Position { get; init; }

        public Ship? OccupiedBy { get; private set; }

        public bool IsOccupied => OccupiedBy != null;

        public bool TryOccupy(Ship ship)
        {
            if (!IsOccupied)
            {
                OccupiedBy = ship;
                return true;
            }

            return false;
        }
    }
}
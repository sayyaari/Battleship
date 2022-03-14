namespace Battleship.Model
{
    public class Ship
    {
        public Ship(ShipSize size, Position startPosition, Direction direction)
        {
            Size = size;
            StartPosition = startPosition;
            Direction = direction;
        }

        public ShipSize Size { get; init; }
        public Position StartPosition { get; set; }
        public Direction Direction { get; set; }

        public override string ToString()
        {
            return $" Ship {Size} at {StartPosition}";
        }
    }
}

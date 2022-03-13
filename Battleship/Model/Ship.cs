namespace Battleship.Model
{
    public class Ship
    {
        public ShipSize Size { get; init; }
        public Position StartPosition { get; set; }
        
        public Direction Direction { get; set; }
    }
}

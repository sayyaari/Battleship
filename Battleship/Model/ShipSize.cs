namespace Battleship.Model
{
    public struct ShipSize
    {
        public ShipSize(int length)
        {
            Length = length;
        }
        public int Length { get; init; }
    }
}
namespace Battleship.Model
{
    public struct ShipSize
    {
        public ShipSize(int length)
        {
            Length = length;
        }
        public int Length { get; init; }

        
        public static implicit operator ShipSize(int length)
        {
            return new ShipSize(length);
        }

        public override string ToString()
        {
            return $"Length={Length}";
        }
    }
}
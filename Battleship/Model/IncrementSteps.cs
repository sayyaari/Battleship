namespace Battleship.Model
{
    public struct IncrementSteps
    {
        public IncrementSteps(Direction direction)
        {
            if (direction == Direction.Horizaontal)
            {
                X = 1;
                Y = 0;
            }
            else
            {
                X = 0;
                Y = 1;
            }

        }

        public int X { get; init; }
        public int Y { get; init; }
    }
}

using Battleship.Model;

namespace Battleship.Validators
{
    internal class PositionValidator : IPositionValidator
    {
        public bool IsValid(Position position, BoardDimension dimension)
        {
            return position.X >= 0 && position.Y >= 0 && position.Y < dimension.Height && position.X < dimension.Width;
        }
    }
}
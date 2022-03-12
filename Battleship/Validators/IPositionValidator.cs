using Battleship.Model;

namespace Battleship.Validators
{
    public interface IPositionValidator
    {
        bool IsValid(Position position, BoardDimension dimension);
    }
}
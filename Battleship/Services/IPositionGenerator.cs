using Battleship.Model;

namespace Battleship.Services
{
    public interface IPositionGenerator
    {
        IEnumerable<Position> Generate(Position originPosition, Direction direction, ShipSize boundary);

        Position NextPosition(Position currentPosition, IncrementSteps steps);

    }
}

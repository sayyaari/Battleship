using Battleship.Model;

namespace Battleship.Services
{
    public class PositionGenerator : IPositionGenerator
    {
        public IEnumerable<Position> Generate(Position originPosition, Direction direction, ShipSize boundary)
        {
            if (boundary.Length <= 0)
                yield break;

            yield return originPosition;

            IncrementSteps steps = new IncrementSteps(direction);
            var currentPosition = originPosition;
            for (int i = 1; i < boundary.Length; i++)
            {
                currentPosition = NextPosition(currentPosition, steps);
                yield return currentPosition;
            }

        }

        public Position NextPosition(Position currentPosition, IncrementSteps steps)
        {
            return new Position(currentPosition.X + steps.X, currentPosition.Y + steps.Y);
        }

    }

}

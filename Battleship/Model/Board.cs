using Battleship.Exceptions;

namespace Battleship.Model
{
    public class Board
    {
        public Board(IBoardGrid boardGrid)
        {
            Grid = boardGrid; ;
           
        }

        internal IBoardGrid Grid { get; init; }

        public bool AddShip(Ship ship, Direction direction, Position startPosition)
        {
            if (!Grid.IsPositionInGrid(startPosition))
                throw new OutOfRangePosition(startPosition);

            throw new NotImplementedException();

        }

        public AttackResult TakeAttack(int x, int y)
        {
            Position position = new(x, y);


            if (Grid.TryGet(position, out var cell))
            {
                return cell.Attack();
            }

            throw new OutOfRangePosition(position);
        }

    }
}

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleship.Tests")]

namespace Battleship.Model
{
    public class Board
    {
        public Board(IBoardGrid boardGrid)
        {
            Grid = boardGrid; ;
        }

        internal IBoardGrid Grid { get; init; }

        public AttackResult TakeAttack(int x, int y)
        {
            Position position = new(x, y);

            if (Grid.TryGet(position, out var cell))
            {
                return cell.Attack();
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            throw new ArgumentOutOfRangeException($"The provided position {position} is not in the board's boundary",(Exception) null );
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

    }
}

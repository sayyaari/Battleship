using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleship.Tests")]

namespace Battleship.Model
{
    public class Board
    {
        public Board(BoardGrid boardGrid)
        {
            Grid = boardGrid; ;
        }

        internal BoardGrid Grid { get; init; }

        //public AttackResult Attack(int x, int y)
        //{
        //    Position position = new(x, y);

        //    var cell = GetCellAt(position);

        //    if (cell == null)
        //        throw new ArgumentOutOfRangeException($"The provided position {position} is not in the board's boundary");


        //    return cell.Attack();

        //}

    }
}

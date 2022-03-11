namespace Battleship.Model
{

    public class Board
    {
        public Board(int dimension)
        {
            Dimension = dimension;
        }
        public int Dimension { get; }

        public AttackResult Attack(int row, int column)
        {
            Position position = new(row, column);
            
            var cell = GetCellAt(position);

            if (cell == null)
                throw new ArgumentOutOfRangeException($"The provider position {position} is not in the board's boundary");

            
            return cell.Attack();

        }

        private Cell GetCellAt(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
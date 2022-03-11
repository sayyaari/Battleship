namespace Battleship.Model
{
    public struct Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; }
        public int Column { get; }

        public override string ToString()
        {
            return $"row={Row} column={Column}";
        }
    }
}
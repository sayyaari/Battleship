using Battleship.Validators;

namespace Battleship.Model
{
    public class BoardGrid : IBoardGrid
    {
        private readonly IPositionValidator _positionValidator;
        public BoardGrid(ICell[,] cells, IPositionValidator positionValidator)
        {
            Cells = cells;
            Dimension = new(cells.GetLength(0), cells.GetLength(1));
            _positionValidator = positionValidator;
        }

        public BoardDimension Dimension { get; init; }

        internal ICell[,] Cells { get; init; }

        public bool TryGet(Position position, out ICell? cell)
        {
            cell = default;
            if (_positionValidator.IsValid(position, Dimension))
            {
                cell = Cells[position.X, position.Y];
                return true;
            }

            return false;
        }
    }
}

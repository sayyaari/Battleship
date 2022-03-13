using Battleship.Exceptions;
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

        public ICell[,] Cells { get; init; }


        public bool IsPositionInGrid(Position position)
        {
            return _positionValidator.IsValid(position, Dimension);
        }

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

        public IEnumerable<ICell> CalculateOccupyingCells(Position startPosition, Direction direction, ShipSize size)
        {
            (int X, int Y) incrementingSteps = CalculateIncrementingSteps(direction);

            var endPosition = CalculatePosition(startPosition, incrementingSteps, size.Length - 1);

            if (IsPositionInGrid(endPosition))
                throw new ShipeNotFittedInBoard();

            List<ICell> area = new();
            for (int i = 0; i < size.Length; i++)
            {
                var position = CalculatePosition(startPosition, incrementingSteps, i);

                if (TryGet(position, out var cell))
                {
                    if (cell.IsOccupied)
                    {
                        throw new ShipeNotFittedInBoard();
                    }
                    else
                        area.Add(cell);
                }


                throw new ShipeNotFittedInBoard();
            }

            return area;
        }

        private static Position CalculatePosition(Position startPosition, (int X, int Y) incrementingSteps, int stepNumber)
        {
            return new Position(startPosition.X + (incrementingSteps.X * stepNumber), startPosition.Y + (incrementingSteps.Y * stepNumber));
        }

        private static (int, int) CalculateIncrementingSteps(Direction direction)
        {
            return direction switch
            {
                Direction.Horizaontal => (1, 0),
                Direction.Vertical => (0, 1),
            };
        }
    }
}

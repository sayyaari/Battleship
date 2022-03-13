using Battleship.Exceptions;
using Battleship.Helpers;
using Battleship.Services;
using Battleship.Validators;
using System.Diagnostics.CodeAnalysis;

namespace Battleship.Model
{
    public class BoardGrid : IBoardGrid
    {
        private readonly IPositionValidator _positionValidator;
        private readonly IPositionGenerator _positionGenerator;
        public BoardGrid(ICell[,] cells, IPositionValidator positionValidator, IPositionGenerator positionGenerator)
        {
            Cells = cells.ThrowIfNull();
            Dimension = BoardDimension.CreateFrom(Cells);
            _positionValidator = positionValidator.ThrowIfNull();
            _positionGenerator = positionGenerator.ThrowIfNull();
        }

        public BoardDimension Dimension { get; init; }

        public ICell[,] Cells { get; init; }


        public bool IsPositionInGrid(Position position)
        {
            return _positionValidator.IsValid(position, Dimension);
        }

        public bool TryGet(Position position,[NotNullWhen(true)] out ICell? cell)
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
            var positions = _positionGenerator.Generate(startPosition, direction, size);
            List<ICell> area = new();
            foreach (var position in positions)
            {
                if (TryGet(position, out var cell))
                {
                    if (cell.IsOccupied)
                    {
                        throw new ShipeNotFittedInBoard();
                    }
                    else
                        area.Add(cell);
                }
                else
                {
                    throw new ShipeNotFittedInBoard();
                }

            }
            

            return area;
        }
    }
}

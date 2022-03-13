using System.Diagnostics.CodeAnalysis;

namespace Battleship.Model.Interfaces
{
    public interface IBoardGrid
    {
        BoardDimension Dimension { get; init; }

        bool TryGet(Position position, [NotNullWhen(true)] out ICell? cell);

        bool IsPositionInGrid(Position position);
        IEnumerable<ICell> CalculateOccupyingCells(Position startPosition, Direction direction, ShipSize size);
    }
}
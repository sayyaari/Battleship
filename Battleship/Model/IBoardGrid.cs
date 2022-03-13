﻿namespace Battleship.Model
{
    public interface IBoardGrid
    {
        BoardDimension Dimension { get; init; }

        bool TryGet(Position position, out ICell? cell);

        bool IsPositionInGrid(Position position);
        IEnumerable<ICell> CalculateOccupyingCells(Position startPosition, Direction direction, ShipSize size);
    }
}
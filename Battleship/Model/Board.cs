using Battleship.Exceptions;

namespace Battleship.Model
{
    public class Board
    {
        private readonly IBoardGrid _grid;
        private readonly List<OccupiedArea> occupiedAreas = new();

        public Board(IBoardGrid boardGrid)
        {
            _grid = boardGrid; ;
        }

        public bool AddShip(AddShipCommand command)
        {
            var (ship, startPosition, direction) = command;

            if (!_grid.IsPositionInGrid(startPosition))
                throw new OutOfRangePosition(startPosition);

            try
            {
                var cells = _grid.CalculateOccupyingCells(startPosition, direction, ship.Size).ToList();

                if (cells.Count > 0)
                {
                    OccupiedArea area = new OccupiedArea(ship, startPosition, direction, cells);

                    occupiedAreas.Add(area);
                }

                return cells.Any();
            }
            catch (ShipeNotFittedInBoard)
            {
                return false;
            }
        }


        public AttackResult TakeAttack(int x, int y)
        {
            Position position = new(x, y);

            if (_grid.TryGet(position, out var cell))
            {
                return cell.Attack();
            }

            throw new OutOfRangePosition(position);
        }

    }

    public record AddShipCommand(Ship Ship, Position StartPosition, Direction Direction);
}

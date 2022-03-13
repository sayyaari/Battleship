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

        public bool AddShip(Ship ship)
        {
            if (!_grid.IsPositionInGrid(ship.StartPosition))
                throw new OutOfRangePosition(ship.StartPosition);

            try
            {
                var cells = _grid.CalculateOccupyingCells(ship.StartPosition, ship.Direction, ship.Size).ToList();

                if (cells.Count > 0)
                {
                    OccupiedArea area = new (ship, cells);

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
}

using Battleship.Model;
using Battleship.Validators;

namespace Battleship.Factories
{
    public class BoardGridFactory : IBoardGridFactory
    {
        private readonly ICellFactory _cellFactory;
        private readonly IPositionValidator _positionValidator;

        public BoardGridFactory(ICellFactory cellFactory, IPositionValidator positionValidator)
        {
            _cellFactory = cellFactory;
            _positionValidator = positionValidator;
        }
        public BoardGrid Create(BoardDimension dimension)
        {
            ICell[,] cells = new Cell[dimension.Width, dimension.Height];
            for (int i = 0; i < dimension.Width; i++)
            {
                for (int j = 0; j < dimension.Height; j++)
                {
                    cells[i, j] = _cellFactory.Create(new Position(i, j));
                }
            }

            return new BoardGrid(cells, _positionValidator);
        }
    }
}

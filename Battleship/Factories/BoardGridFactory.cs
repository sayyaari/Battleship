using Battleship.Model;
using Battleship.Model.Interfaces;
using Battleship.Services;
using Battleship.Validators;

namespace Battleship.Factories
{
    public class BoardGridFactory : IBoardGridFactory
    {
        private readonly ICellFactory _cellFactory;
        private readonly IPositionValidator _positionValidator;
        private readonly IPositionGenerator _positionGenerator;

        public BoardGridFactory(ICellFactory cellFactory, IPositionValidator positionValidator, IPositionGenerator positionGenerator)
        {
            _cellFactory = cellFactory;
            _positionValidator = positionValidator;
            _positionGenerator = positionGenerator;
        }
        public IBoardGrid Create(BoardDimension dimension)
        {
            ICell[,] cells = new Cell[dimension.Width, dimension.Height];
            for (int i = 0; i < dimension.Width; i++)
            {
                for (int j = 0; j < dimension.Height; j++)
                {
                    cells[i, j] = _cellFactory.Create(new Position(i, j));
                }
            }

            return new BoardGrid(cells, _positionValidator, _positionGenerator);
        }
    }
}

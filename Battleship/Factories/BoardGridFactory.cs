using Battleship.Model;

namespace Battleship.Factories
{
    public class BoardGridFactory : IBoardGridFactory
    {
        private readonly ICellFactory _cellFactory;

        public BoardGridFactory(ICellFactory cellFactory)
        {
            _cellFactory = cellFactory;
        }
        public BoardGrid Create(BoardDimension dimension)
        {
            Cell[,] cells = new Cell[dimension.Width, dimension.Height];
            for (int i = 0; i < dimension.Width; i++)
            {
                for (int j = 0; j < dimension.Height; j++)
                {
                    cells[i, j] = _cellFactory.Create(new Position(i, j));
                }
            }
            return new BoardGrid(cells);
        }
    }
}

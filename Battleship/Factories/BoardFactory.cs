using Battleship.Model;

namespace Battleship.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IBoardGridFactory _gridFactory;

        public BoardFactory(IBoardGridFactory boardGridFactory)
        {
            _gridFactory = boardGridFactory;
        }

        public Board Create(BoardDimension dimension)
        {
            var grid = _gridFactory.Create(dimension);
            return new Board(grid);
        }
    }

}

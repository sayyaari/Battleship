using Battleship.Model;
using Battleship.Model.Interfaces;

namespace Battleship.Factories
{
    internal class BoardFactory : IBoardFactory
    {
        private readonly IBoardGridFactory _gridFactory;

        public BoardFactory(IBoardGridFactory boardGridFactory)
        {
            _gridFactory = boardGridFactory;
        }

        public IBoard Create(BoardDimension dimension)
        {
            var grid = _gridFactory.Create(dimension);
            return new Board(grid);
        }
    }

}

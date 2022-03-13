using Battleship.Model;
using Battleship.Model.Interfaces;

namespace Battleship.Factories
{
    public interface IBoardFactory
    {
        IBoard Create(BoardDimension dimension);
    }
}

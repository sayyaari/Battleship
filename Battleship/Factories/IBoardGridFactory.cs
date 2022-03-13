using Battleship.Model;
using Battleship.Model.Interfaces;

namespace Battleship.Factories
{
    public interface IBoardGridFactory
    {
        IBoardGrid Create(BoardDimension dimension);
    }
}

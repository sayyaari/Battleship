using Battleship.Model;

namespace Battleship.Factories
{
    public interface IBoardGridFactory
    {
        BoardGrid Create(BoardDimension dimension);
    }
}

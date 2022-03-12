using Battleship.Model;

namespace Battleship.Factories
{
    public interface ICellFactory
    {
        ICell Create(Position position);

    }

}

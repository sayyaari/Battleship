using Battleship.Model;

namespace Battleship.Factories
{
    public interface ICellFactory
    {
        Cell Create(Position position);

    }

}

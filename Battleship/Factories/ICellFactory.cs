using Battleship.Model;
using Battleship.Model.Interfaces;

namespace Battleship.Factories
{
    public interface ICellFactory
    {
        ICell Create(Position position);

    }

}

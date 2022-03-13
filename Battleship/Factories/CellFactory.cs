using Battleship.Model;
using Battleship.Model.Interfaces;

namespace Battleship.Factories
{
    internal class CellFactory : ICellFactory
    {
        public ICell Create(Position position)
        {
            return new Cell(position);
        }
    }

}

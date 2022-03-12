using Battleship.Model;

namespace Battleship.Factories
{
    public class CellFactory : ICellFactory
    {
        public Cell Create(Position position)
        {
            return new Cell(position);
        }
    }

}

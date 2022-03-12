using Battleship.Model;

namespace Battleship.Factories
{
    public class CellFactory : ICellFactory
    {
        public ICell Create(Position position)
        {
            return new Cell(position);
        }
    }

}

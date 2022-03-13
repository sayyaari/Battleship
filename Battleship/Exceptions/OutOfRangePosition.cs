using Battleship.Model;

namespace Battleship.Exceptions
{
    public class OutOfRangePosition : Exception
    {
        public OutOfRangePosition(Position position):base($"The provided position {position} is not in the board's boundary")
        {
        }

    }
    public class ShipeNotFittedInBoard : Exception
    {
        public ShipeNotFittedInBoard()
        {
        }
    }
}

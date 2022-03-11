using Battleship.Model;
using MediatR;

namespace Battleship.Commands
{
    public class CreateBoard : IRequest<Result<Board>>
    {
        public CreateBoard(int dimension)
        {
            Dimension = dimension;
        }
        public int Dimension { get; }
    }
}

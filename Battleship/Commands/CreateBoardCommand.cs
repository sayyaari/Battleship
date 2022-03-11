using Battleship.Model;
using MediatR;

namespace Battleship.Commands
{
    public class CreateBoardCommand : IRequest<CommandResult<Board>>
    {
        public CreateBoardCommand(int dimension)
        {
            Dimension = dimension;
        }
        public int Dimension { get; }
    }
}

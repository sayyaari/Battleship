using Battleship.Commands;
using Battleship.Model;
using MediatR;

namespace Battleship.CommandHandlers
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, CommandResult<Board>>
    {
        public async Task<CommandResult<Board>> Handle(CreateBoardCommand request, CancellationToken cancellationToken = default)
        {
            var result = await Task.FromResult(new Board(request.Dimension).ToCommandResult());
            return result;
        }
    }
}

using Battleship.Commands;
using Battleship.Model;
using MediatR;

namespace Battleship.CommandHandlers
{
    public class CreateBoardHandler : IRequestHandler<CreateBoard, Result<Board>>
    {
        public async Task<Result<Board>> Handle(CreateBoard request, CancellationToken cancellationToken = default)
        {
            var result = await Task.FromResult(new Board(request.Dimension).ToCommandResult());
            return result;
        }
    }
}

using Battleship.Commands;
using Battleship.Factories;
using Battleship.Model;
using MediatR;

namespace Battleship.CommandHandlers
{
    public class CreateBoardHandler : IRequestHandler<CreateBoard, Result<Board>>
    {
        private readonly IBoardFactory _boardFactory;

        public CreateBoardHandler(IBoardFactory boardFactory)
        {
            
            _boardFactory = boardFactory;
        }
        public async Task<Result<Board>> Handle(CreateBoard request, CancellationToken cancellationToken = default)
        {
            var result = await Task.FromResult(_boardFactory.Create(new BoardDimension(request.Width, request.Height)).ToCommandResult());
            return result;
        }
    }
}

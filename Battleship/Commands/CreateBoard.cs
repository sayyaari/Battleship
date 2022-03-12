using Battleship.Model;
using MediatR;

namespace Battleship.Commands
{
    public class CreateBoard : IRequest<Result<Board>>
    {
        public CreateBoard(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; init; }
        public int Height { get; init; }
    }
}

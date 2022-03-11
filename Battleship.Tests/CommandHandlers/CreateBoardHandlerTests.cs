using Battleship.CommandHandlers;
using Battleship.Commands;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.CommandHandlers
{
    public class CreateBoardHandlerTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async void Test1(int boardSize)
        {
            var commandHandler = new CreateBoardHandler();

            var result = await commandHandler.Handle(new CreateBoard(boardSize));         

            result?.Value?.Dimension.Should().Be(boardSize);
        }
    }
}
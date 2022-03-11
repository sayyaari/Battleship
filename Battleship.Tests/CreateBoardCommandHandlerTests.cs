using Battleship.CommandHandlers;
using Battleship.Commands;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests
{
    public class CreateBoardCommandHandlerTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async void Test1(int boardSize)
        {
            var commandHandler = new CreateBoardCommandHandler();

            var result = await commandHandler.Handle(new CreateBoardCommand(boardSize));         

            result?.Result?.Dimension.Should().Be(boardSize);
        }
    }
}
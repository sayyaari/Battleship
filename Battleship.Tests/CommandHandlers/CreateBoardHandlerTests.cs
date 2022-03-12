using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.CommandHandlers;
using Battleship.Commands;
using Battleship.Factories;
using Battleship.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleship.Tests.CommandHandlers
{
    public class CreateBoardHandlerTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<IBoardFactory> _boardFactory;

        public CreateBoardHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _boardFactory = new Mock<IBoardFactory>(MockBehavior.Strict);
        }

        [Fact]
        
        public async void Test1()
        {
            var commandHandler = new CreateBoardHandler(_boardFactory.Object);
            var dimension = _fixture.Create<BoardDimension>();

            Board generatedBoard = _fixture.Create<Board>();
            _boardFactory.Setup(x => x.Create(It.Is<BoardDimension>(d => d.Equals(dimension))))
                .Returns(generatedBoard);
                

            var result = await commandHandler.Handle(new CreateBoard(dimension.Width, dimension.Height));

            result.Value.Should().NotBeNull();
            result.Value.Should().Be(generatedBoard);
        }
    }
}
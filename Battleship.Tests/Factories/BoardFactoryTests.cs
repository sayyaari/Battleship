using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Factories;
using Battleship.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleship.Tests.Factories
{
    public class BoardFactoryTests
    {
        private readonly IFixture _fixture;

        public BoardFactoryTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

            [Fact]
        public void Should_Create_Cell()
        {

            BoardDimension dimension = _fixture.Create<BoardDimension>();

            BoardGrid grid = _fixture.Create<BoardGrid>();

            Mock<IBoardGridFactory> gridFactory = SetupGridFactoryMock(dimension, grid);

            BoardFactory factory = new(gridFactory.Object);



            var board = factory.Create(dimension);



            board.Should().NotBeNull();

        }

        private static Mock<IBoardGridFactory> SetupGridFactoryMock(BoardDimension dimension, BoardGrid grid)
        {
            var gridFactory = new Mock<IBoardGridFactory>(MockBehavior.Strict);
            gridFactory.Setup(x => x.Create(It.IsAny<BoardDimension>()))
                .Returns(grid)
                .Callback((BoardDimension d) =>
                {
                    d.Should().Be(dimension);
                });
            return gridFactory;
        }
    }
}

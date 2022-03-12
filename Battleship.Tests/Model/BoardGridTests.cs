using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Model;
using Battleship.Validators;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardGridTests
    {
        private readonly IFixture _fixture;
        private Mock<IPositionValidator> _positionValidator;
        private readonly ICell[,] _cells;
        private BoardGrid _grid ;
        private readonly Random _rand ;

        public BoardGridTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _positionValidator = new Mock<IPositionValidator>();
            _cells = _fixture.Create<ICell[,]>();
            _rand = new Random();
            _grid = new BoardGrid(_cells, _positionValidator.Object);
        }


        [Fact]
        public void Should_Calculate_Correct_Dimension()
        {
            _grid.Dimension.Should().Be(new BoardDimension(_cells.GetLength(0), _cells.GetLength(1)));
        }


        [Fact]
        public void Should_Set_Cells()
        {
            _grid.Cells.Should().Be(_cells);
        }


        [Fact]
        public void Should_Return_False_When_Accessing_Invalid_Position()
        {
            var position = SetupTestForTryGet(false);

            var canGetCells = _grid.TryGet(position, out var cell);

            canGetCells.Should().BeFalse();

            cell.Should().BeNull();

            _positionValidator.VerifyAll();
        }


        [Fact]
        public void Should_Return_True_When_Accessing_Valid_Position()
        {
            var position = SetupTestForTryGet(true);

            var canGetCells = _grid.TryGet(position, out var cell);

            canGetCells.Should().BeTrue();

            cell.Should().Be(_cells[position.X, position.Y]);

            _positionValidator.VerifyAll();
        }


        private Position SetupTestForTryGet(bool positionValidation)
        {
            _positionValidator = new Mock<IPositionValidator>(MockBehavior.Strict);

            _grid = new BoardGrid(_cells, _positionValidator.Object);

            var position = new Position(_rand.Next(0, _grid.Dimension.Width-1), _rand.Next(0, _grid.Dimension.Height- 1));

            _positionValidator.Setup(x => x.IsValid(It.IsAny<Position>(), It.IsAny<BoardDimension>()))
                .Callback((Position p, BoardDimension d) =>
                {
                    p.Should().Be(position);
                    d.Should().Be(_grid.Dimension);
                })
                .Returns(positionValidation);


            return position;
        }
    }
}

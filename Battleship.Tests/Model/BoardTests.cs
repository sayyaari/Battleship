using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Exceptions;
using Battleship.Model;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardTests
    {
        private readonly IFixture _fixture;
        private Board _board;
        private Mock<IBoardGrid> _boardGrid;
        private Mock<ICell> _cellUnderAttack;

        public BoardTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _boardGrid = new Mock<IBoardGrid>(MockBehavior.Strict);
            _board = new Board(_boardGrid.Object);
            _cellUnderAttack = new Mock<ICell>(MockBehavior.Strict);

        }


        [Theory]
        [InlineData(AttackResult.Miss)]
        [InlineData(AttackResult.Hit)]
        public void Should_Attack_Return_ExpectedResult(AttackResult expectedAttackResult)
        {
            var attackPosition = SetupBoardGridForAttack(expectedAttackResult);


            var attackResult = _board.TakeAttack(attackPosition.X, attackPosition.Y);


            attackResult.Should().Be(expectedAttackResult);

            _cellUnderAttack.VerifyAll();

            _boardGrid.VerifyAll();
        }


        [Fact]
        public void Should_Attack_Throw_Exception_When_Attack_Position_Is_Invalid()
        {
            SetupBoardGridToReturnFalseWhenAccessingCell();

            Position attackPosition = _fixture.Create<Position>();


            Action act = () => _board.TakeAttack(attackPosition.X, attackPosition.Y);


            act.Should().Throw<OutOfRangePosition>();
            _boardGrid.VerifyAll();
        }

        [Fact]
        public void Should_AddShip_Throw_Exception_When_Position_Is_Invalid()
        {
            var ship = _fixture.Create<AddShipCommand>();
            var startPosition = ship.StartPosition;

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(false)
                .Callback<Position>(position => position.Should().Be(startPosition));


            Action act = () => _board.AddShip(_fixture.Create<AddShipCommand>());


            act.Should().Throw<OutOfRangePosition>();
            _boardGrid.VerifyAll();
        }

        [Fact]
        public void Should_AddShip_Should_Return_False_When_No_Occupying_Cells_Returned()
        {
            var shipCommand = SetupBoardGridForCalculationOccupyingCells(0);

            var shipAdded = _board.AddShip(shipCommand);

            shipAdded.Should().BeFalse();
            _boardGrid.VerifyAll();
        }

        [Fact]
        public void Should_AddShip_Should_Return_False_When_ShipNotFittedInBoard_Exception_Caught()
        {
            var shipCommand = SetupBoardGridForThrowingShipNotFittedInBoardException();

            var shipAdded = _board.AddShip(shipCommand);

            shipAdded.Should().BeFalse();
            _boardGrid.VerifyAll();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        public void Should_AddShip_Should_Return_False_When_Occupying_Cells_Have_Any_Value(int cellsNumber)
        {
            var shipCommand = SetupBoardGridForCalculationOccupyingCells(cellsNumber);

            var shipAdded = _board.AddShip(shipCommand);

            shipAdded.Should().BeTrue();
            _boardGrid.VerifyAll();
        }


        private AddShipCommand SetupBoardGridForCalculationOccupyingCells(int cellsNumber)
        {
            var shipCommand = _fixture.Create<AddShipCommand>();

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(true);


            _boardGrid.Setup(x => x.CalculateOccupyingCells(It.IsAny<Position>(), It.IsAny<Direction>(), It.IsAny<ShipSize>()))
                .Callback<Position, Direction, ShipSize>((position, direction, size) =>
                {
                    position.Should().Be(shipCommand.StartPosition);
                    direction.Should().Be(shipCommand.Direction);
                    size.Should().Be(shipCommand.Ship.Size);
                })
                .Returns(Enumerable.Range(0,cellsNumber).Select(i=>_fixture.Create<ICell>()));


            return shipCommand;
        }

        private AddShipCommand SetupBoardGridForThrowingShipNotFittedInBoardException()
        {
            var shipCommand = _fixture.Create<AddShipCommand>();

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(true);


            _boardGrid.Setup(x => x.CalculateOccupyingCells(It.IsAny<Position>(), It.IsAny<Direction>(), It.IsAny<ShipSize>()))
                .Throws<ShipeNotFittedInBoard>();


            return shipCommand;
        }


        private void SetupBoardGridToReturnFalseWhenAccessingCell()
        {
            ICell returnedCellObject = null;

            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject))
                .Returns(false);
        }

        private Position SetupBoardGridForAttack(AttackResult expectedAttackResult)
        {
            // Set up the Cell taking attack
            _cellUnderAttack.Setup(x => x.Attack()).Returns(expectedAttackResult);


            Position attackPosition = _fixture.Create<Position>();


            // Set up BoardGrid
            ICell returnedCellObject = null;
            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject))
                .Callback((Position p, out ICell cell) =>
                {
                    p.Should().Be(attackPosition);
                    cell = this._cellUnderAttack.Object;
                })
                .Returns(true);

            return attackPosition;
        }
    }
}

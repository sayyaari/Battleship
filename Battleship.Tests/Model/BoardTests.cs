using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Exceptions;
using Battleship.Model;
using Battleship.Model.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardTests
    {
        private readonly IFixture _fixture;
        private readonly Board _board;
        private readonly Mock<IBoardGrid> _boardGrid;
        private readonly Mock<ICell> _cellUnderAttack;

        public BoardTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _boardGrid = new Mock<IBoardGrid>(MockBehavior.Strict);
            _board = new Board(_boardGrid.Object);
            _cellUnderAttack = new Mock<ICell>(MockBehavior.Strict);

        }

        [Fact]
        public void Should_Throw_When_Passed_BoardGrid_IsNull()
        {
            Action act = () =>
            {
                Board board = new (null!);
            };

            act.Should().Throw<ArgumentNullException>();
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
            var ship = _fixture.Create<Ship>();
            var startPosition = ship.StartPosition;

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(false)
                .Callback<Position>(position => position.Should().Be(startPosition));


            Action act = () => _board.AddShip(ship);


            act.Should().Throw<OutOfRangePosition>();
            _boardGrid.VerifyAll();
        }


        [Fact]
        public void Should_AddShip_Should_Return_False_When_ShipNotFittedInBoard_Exception_Caught()
        {
            var ship = SetupBoardGridForThrowingShipNotFittedInBoardException();

            var shipAdded = _board.AddShip(ship);

            shipAdded.Should().BeFalse();
            _boardGrid.VerifyAll();
        }

        [Fact]
        public void Should_AddShip_Should_Return_False_When_No_Occupying_Cells_Returned()
        {
            var (ship, occupiedCells) = SetupBoardGridForCalculationOccupyingCells(0);
            occupiedCells.Should().BeEmpty();

            var shipAdded = _board.AddShip(ship);

            shipAdded.Should().BeFalse();
            _boardGrid.VerifyAll();

        }


        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        public void Should_AddShip_Should_Return_True_When_Occupying_Cells_Have_Some_Items(int cellsNumber)
        {
            var (ship, occupiedCells) = SetupBoardGridForCalculationOccupyingCells(cellsNumber);
            occupiedCells.Should().NotBeEmpty();



            var shipAdded = _board.AddShip(ship);


            shipAdded.Should().BeTrue();

            _board.OccupiedAreas.Should().HaveCount(1);

            var occupiedArea = _board.OccupiedAreas.First();
            occupiedArea.Cells.Should().BeEquivalentTo(occupiedCells);
            occupiedArea.Ship.Should().Be(ship);
            occupiedArea.HasSunkShip.Should().BeFalse();

            _boardGrid.VerifyAll();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void Should_HasLost_Return_True_When_All_Ships_In_Occupied_Aread_Have_Sunk(int occupiedAreaNumber)
        {
            var generatedOccupiedArea = Enumerable.Range(1, occupiedAreaNumber).Select(i =>
            {
                var occupiedArea = new Mock<IOccupiedArea>(MockBehavior.Strict);
                occupiedArea.Setup(a => a.HasSunkShip).Returns(true);
                return occupiedArea.Object;
            }).ToList();

            _board.OccupiedAreas.AddRange(generatedOccupiedArea);


            _board.HasLost.Should().BeTrue();
        }


        [Fact]
        public void Should_HasLost_Return_False_When_At_Leas_One_Aread_ُShip_Has_Not_Sunk()
        {
            var generatedOccupiedAreas = Enumerable.Range(1, 5).Select(i =>
            {
                var occupiedArea = new Mock<IOccupiedArea>(MockBehavior.Strict);
                occupiedArea.Setup(a => a.HasSunkShip).Returns(() => i != 3);
                return occupiedArea.Object;
            }).ToList();

            _board.OccupiedAreas.AddRange(generatedOccupiedAreas);


            _board.HasLost.Should().BeFalse();
        }

        private (Ship, IEnumerable<ICell>) SetupBoardGridForCalculationOccupyingCells(int cellsNumber)
        {
            var ship = _fixture.Create<Ship>();

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(true);


            IEnumerable<ICell> occupiedCells = Enumerable.Range(0, cellsNumber).Select(i => _fixture.Create<ICell>()).ToList();

            _boardGrid.Setup(x => x.CalculateOccupyingCells(It.IsAny<Position>(), It.IsAny<Direction>(), It.IsAny<ShipSize>()))
                .Callback<Position, Direction, ShipSize>((position, direction, size) =>
                {
                    position.Should().Be(ship.StartPosition);
                    direction.Should().Be(ship.Direction);
                    size.Should().Be(ship.Size);
                })
                .Returns(occupiedCells);


            return (ship, occupiedCells);
        }

        private Ship SetupBoardGridForThrowingShipNotFittedInBoardException()
        {
            var shipCommand = _fixture.Create<Ship>();

            _boardGrid.Setup(x => x.IsPositionInGrid(It.IsAny<Position>())).Returns(true);


            _boardGrid.Setup(x => x.CalculateOccupyingCells(It.IsAny<Position>(), It.IsAny<Direction>(), It.IsAny<ShipSize>()))
                .Throws<ShipeNotFittedInBoard>();


            return shipCommand;
        }


        private void SetupBoardGridToReturnFalseWhenAccessingCell()
        {
            ICell returnedCellObject;

            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject!))
                .Returns(false);
        }

        private Position SetupBoardGridForAttack(AttackResult expectedAttackResult)
        {
            // Set up the Cell taking attack
            _cellUnderAttack.Setup(x => x.Attack()).Returns(expectedAttackResult);


            Position attackPosition = _fixture.Create<Position>();


            // Set up BoardGrid
            ICell returnedCellObject;
            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject!))
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

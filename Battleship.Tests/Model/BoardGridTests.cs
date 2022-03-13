using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Exceptions;
using Battleship.Model;
using Battleship.Services;
using Battleship.Tests.Utils;
using Battleship.Validators;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardGridTests
    {
        private readonly IFixture _fixture;
        private Mock<IPositionValidator> _positionValidator;
        private Mock<IPositionGenerator> _positionGenerator;
        private readonly ICell[,] _cells;
        private BoardGrid _grid;
        private readonly Random _rand;

        public BoardGridTests()
        {
            _fixture = new Fixture() { RepeatCount = 10 }.Customize(new AutoMoqCustomization());
            _positionValidator = new Mock<IPositionValidator>();
            _positionGenerator = new Mock<IPositionGenerator>();
            _cells = _fixture.Create<ICell[,]>();
            _rand = new Random();
            _grid = new BoardGrid(_cells, _positionValidator.Object, _positionGenerator.Object);
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

                        
            var canGetCell = _grid.TryGet(position, out var cell);
                       
            
            
            canGetCell.Should().BeFalse();
            cell.Should().BeNull();
            _positionValidator.VerifyAll();
        }


        [Fact]
        public void Should_Return_True_When_Accessing_Valid_Position()
        {
            var position = SetupTestForTryGet(true);

            
            var canGetCell = _grid.TryGet(position, out var cell);


            canGetCell.Should().BeTrue();
            cell.Should().Be(_cells[position.X, position.Y]);
            _positionValidator.VerifyAll();
        }

        [Fact]
        public void Should_Calculated_Occupying_Cells()
        {
            var (startPosition, direction, shipSize) = GenerateCalculateOccupyingCellsArguments();
            var generatedPositions = SetupTestToGenerateOccupyingPosition(startPosition, shipSize, direction);


            var occupyingCells = _grid.CalculateOccupyingCells(startPosition, direction, shipSize);


            var expectedCalculatedCells = generatedPositions.Select(p => _cells[p.X, p.Y]).ToList();
            occupyingCells.Should().BeEquivalentTo(expectedCalculatedCells);
            _positionGenerator.VerifyAll();
            _positionValidator.VerifyAll();

        }

        [Fact]
        public void Should_Throw_Exception_When_Any_Of_Calculated_Cell_Is_Occupied()
        {
            var (startPosition, direction, shipSize) = GenerateCalculateOccupyingCellsArguments();
            var generatedPositions = SetupTestToGenerateOccupyingPosition(startPosition, shipSize, direction);
            var occupiedCell = SetupOneOfExpectedCellsAsOccupied(generatedPositions);
            
            
            Action act = () => _grid.CalculateOccupyingCells(startPosition, direction, shipSize);

            act.Should().Throw<ShipeNotFittedInBoard>();
            _positionGenerator.VerifyAll();
            _positionValidator.VerifyAll();
            occupiedCell.VerifyAll();
        }


        [Fact]
        public void Should_Throw_Exception_When_Any_Of_Cell_Is_Out_Of_Board_Range()
        {
            var (startPosition, direction, shipSize) = GenerateCalculateOccupyingCellsArguments();
            var generatedPositions = SetupTestToGenerateOccupyingPosition(startPosition, shipSize, direction, false);

            Action act = () => _grid.CalculateOccupyingCells(startPosition, direction, shipSize);

            act.Should().Throw<ShipeNotFittedInBoard>();
            _positionGenerator.VerifyAll();
            _positionValidator.VerifyAll();

        }


        private Mock<ICell> SetupOneOfExpectedCellsAsOccupied(Position[] generatedPositions)
        {
            var occupyOneOfCalculatedCell = Generator.GenerateNumber(0, generatedPositions.Length - 1);
            var occupiedCell = new Mock<ICell>(MockBehavior.Strict);
            occupiedCell.Setup(c => c.IsOccupied).Returns(true);
            _cells[generatedPositions[occupyOneOfCalculatedCell].X, generatedPositions[occupyOneOfCalculatedCell].Y] = occupiedCell.Object;
            return occupiedCell;
        }



        private Position[] SetupTestToGenerateOccupyingPosition(Position startPosition, ShipSize shipSize, Direction direction, bool allInsideGrid = true)
        {
            _positionValidator = new Mock<IPositionValidator>(MockBehavior.Strict);
            _positionGenerator = new Mock<IPositionGenerator>(MockBehavior.Strict);
            _grid = new BoardGrid(_cells, _positionValidator.Object, _positionGenerator.Object);

            Position[] generatedPositions = GenerateSomePositions();

            _positionGenerator.Setup(x => x.Generate(It.IsAny<Position>(), It.IsAny<Direction>(), It.IsAny<ShipSize>()))
                .Callback((Position p, Direction d, ShipSize s) =>
                {
                    p.Should().Be(startPosition);
                    d.Should().Be(direction);
                    s.Should().Be(shipSize);
                })
                .Returns(generatedPositions);

            
            if (allInsideGrid)
            {
                _positionValidator.Setup(x => x.IsValid(It.IsAny<Position>(), It.IsAny<BoardDimension>()))
                    .Returns(true);
            }
            else
            {
                // Setup the validator in order to return false for at least one of the positions
                var outOfBoardPositionIndex = Generator.GenerateNumber(max: generatedPositions.Length - 1);
                var positionIndex = -1;
                _positionValidator.Setup(x => x.IsValid(It.IsAny<Position>(), It.IsAny<BoardDimension>()))
                    .Callback(() => positionIndex++)
                    .Returns(() => positionIndex == outOfBoardPositionIndex);

            }


            return generatedPositions;

        }

        private static Position[] GenerateSomePositions()
        {
            return new Position[4] {
                Generator.GeneratePosition(0,4,0,4),
                Generator.GeneratePosition(0, 4, 5,9),
                Generator.GeneratePosition(5, 9, 0,4),
                Generator.GeneratePosition(5, 9, 5, 9)
            };
        }

        private (Position, Direction, ShipSize) GenerateCalculateOccupyingCellsArguments()
        {
            Position startPosition = _fixture.Create<Position>();
            ShipSize shipSize = _fixture.Create<ShipSize>();
            Direction direction = _fixture.Create<Direction>();
            return (startPosition, direction, shipSize);

        }
        private Position SetupTestForTryGet(bool positionValidation)
        {
            _positionValidator = new Mock<IPositionValidator>(MockBehavior.Strict);

            _grid = new BoardGrid(_cells, _positionValidator.Object, _positionGenerator.Object);

            var position = new Position(_rand.Next(0, _grid.Dimension.Width - 1), _rand.Next(0, _grid.Dimension.Height - 1));

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

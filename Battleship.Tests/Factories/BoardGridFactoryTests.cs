using AutoFixture;
using Battleship.Factories;
using Battleship.Model;
using Battleship.Services;
using Battleship.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Battleship.Tests.Factories
{
    public class BoardGridFactoryTests
    {
        Fixture _fixture;
        private Mock<ICellFactory> _cellFactory;
        private readonly Mock<IPositionValidator> _positionValidator;
        private readonly BoardDimension _dimension;
        private readonly Mock<IPositionGenerator> _positionGenerator;
        private BoardGridFactory _factory;

        public BoardGridFactoryTests()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 100));

            _cellFactory = new Mock<ICellFactory>();

            _positionValidator = new Mock<IPositionValidator>();

            _dimension = new BoardDimension(_fixture.Create<int>(), _fixture.Create<int>());

            _positionGenerator = new Mock<IPositionGenerator>();
            
            _factory = new(_cellFactory.Object,new Mock<IPositionValidator>().Object, _positionGenerator.Object);

        }

        [Fact]
        public void Should_Constructed_Grid_Have_Correct_Dimension()
        {
            var grid = _factory.Create(_dimension);

            grid.Dimension.Should().Be(_dimension);

        }


        [Fact]
        public void Should_Generated_Cells_Be_In_Correct_Position()
        {

            _cellFactory = new Mock<ICellFactory>(MockBehavior.Strict);

            Dictionary<(int, int), ICell> generatedCells = new();

            _cellFactory.Setup(x => x.Create(It.Is<Position>(position =>
                position.X >= 0 && position.X < _dimension.Width && position.Y >= 0 && position.Y < _dimension.Height)))
            .Returns<Position>(p =>
            {
                Cell cell = new(p);
                generatedCells.Add((cell.Position.X, cell.Position.Y), cell);

                return cell;
            });

            _factory = new(_cellFactory.Object, _positionValidator.Object, _positionGenerator.Object);


            var grid = _factory.Create(_dimension);


            grid.Cells.Should().NotBeNull();

            VerifyGeneratedCellsPosition(generatedCells, grid);

            _cellFactory.Verify(x => x.Create(It.IsAny<Position>()), Times.Exactly(_dimension.Width * _dimension.Height));

        }

        private void VerifyGeneratedCellsPosition(Dictionary<(int, int), ICell> generatedCells, BoardGrid grid)
        {
            for (int i = 0; i < _dimension.Width; i++)
            {
                for (int j = 0; j < _dimension.Height; j++)
                {
                    var cell = grid.Cells[i, j];

                    cell.Should().NotBeNull();

                    cell.Position.Should().Be(new Position(i, j));

                    cell.Should().Be(generatedCells[(i, j)]);

                    cell.IsOccupied.Should().BeFalse();
                    
                    cell.HasHit.Should().BeFalse();

                }
            }
        }
    }
}

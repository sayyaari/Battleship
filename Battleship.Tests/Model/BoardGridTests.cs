using AutoFixture;
using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardGridTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_Be_Constructed_With_Correct_Dimention()
        {
            int width = _fixture.Create<int>();
            int height = _fixture.Create<int>();

            Cell[,] cells = new Cell[width, height];

            var grid = new BoardGrid(cells);

            grid.Cells.Should().Be(cells);
            grid.Dimension.Should().Be(new BoardDimension(width, height));
        }
    }
}

using AutoFixture;
using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class PositionTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_Set_Row_And_Column()
        {
            int row = _fixture.Create<int>();
            int column = _fixture.Create<int>();

            Position position = new(row, column);

            position.Row.Should().Be(row);
            position.Column.Should().Be(column);

        }
    }
}

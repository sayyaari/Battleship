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
        public void Should_Set_X_And_Y()
        {
            int x = _fixture.Create<int>();
            int y = _fixture.Create<int>();

            Position position = new(x, y);

            position.X.Should().Be(x);
            position.Y.Should().Be(y);

        }
    }
}

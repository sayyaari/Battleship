using AutoFixture;
using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardDimentionTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Should_Set_Width_And_Height()
        {
            int width = _fixture.Create<int>();
            int height = _fixture.Create<int>();

            BoardDimension position = new(width, height);

            position.Width.Should().Be(width);
            position.Height.Should().Be(height);

        }

        [Fact]
        public void Should_Width_And_Height_Be_Same_When_Constructed_With_One_Size()
        {
            int size = _fixture.Create<int>();

            BoardDimension position = new(size);

            position.Width.Should().Be(size);
            position.Height.Should().Be(size);

        }
    }
}

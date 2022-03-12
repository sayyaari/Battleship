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

            BoardDimension dimension = new(width, height);

            dimension.Width.Should().Be(width);
            dimension.Height.Should().Be(height);

        }

        [Fact]
        public void Should_Width_And_Height_Be_Same_When_Constructed_With_One_Size()
        {
            int size = _fixture.Create<int>();

            BoardDimension dimension = new(size);

            dimension.Width.Should().Be(size);
            dimension.Height.Should().Be(size);

        }
    }
}

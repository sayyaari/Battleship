using AutoFixture;
using Battleship.Factories;
using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Factories
{
    public class CellFactoryTests
    {
        private readonly Fixture _fixture = new ();

        [Fact]
        public void Should_Create_Cell()
        {

            CellFactory factory = new();
            Position position = new(_fixture.Create<int>(), _fixture.Create<int>());

            var cell = factory.Create(position);

            cell.Position.Should().Be(position);

            cell.IsOccupied.Should().BeFalse();

            cell.OccupiedBy.Should().BeNull();

        }
    }
}

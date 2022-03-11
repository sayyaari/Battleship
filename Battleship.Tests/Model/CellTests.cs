using AutoFixture;
using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class CellTests
    {
        private readonly Fixture _fixture = new Fixture();


        [Fact]
        public void Should_Initialize_Correctly()
        {
            var position = CreatePosition();
            Cell cell = new(position);
            cell.Position.Should().Be(position);
            cell.Position.Should().NotBeSameAs(position);
        }

        [Fact]
        public void Should_Occupy_Cell_When_Is_Free()
        {
            Cell cell = new(CreatePosition());
            cell.IsOccupied.Should().BeFalse();

            var ship = new Ship();
            var didOccupy = cell.TryOccupy(ship);


            didOccupy.Should().BeTrue();
            cell.IsOccupied.Should().BeTrue();
            cell.OccupiedBy.Should().Be(ship);
        }


        [Fact]
        public void Should_Not_Be_Able_To_Occupy_Cell_When_Is_Already_Occupied()
        {
            Cell cell = new(CreatePosition());

            Ship firstShip = new();
            cell.TryOccupy(firstShip);

            var didOccupyByNewShip = cell.TryOccupy(new Ship());


            didOccupyByNewShip.Should().BeFalse();
            cell.OccupiedBy.Should().Be(firstShip);
        }

        [Fact]
        public void Should_Attack_Cell_Return_Hit_When_Occupied()
        {
            Cell cell = new(CreatePosition());
            Ship firstShip = new();
            cell.TryOccupy(firstShip);
            
            
            var result = cell.Attack();

            result.Should().Be(AttackResult.Hit);

        }

        [Fact]
        public void Should_Attack_Cell_Return_Hit_When_Not_Occupied()
        {
            Cell cell = new(CreatePosition());
            Ship firstShip = new();


            var result = cell.Attack();

            result.Should().Be(AttackResult.Miss);

        }


        private Position CreatePosition() => new(_fixture.Create<int>(), _fixture.Create<int>());
    }
}

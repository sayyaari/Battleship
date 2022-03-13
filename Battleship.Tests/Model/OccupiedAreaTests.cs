using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Battleship.Tests.Model
{
    public class OccupiedAreaTests
    {
        private readonly IFixture _fixture;

        public OccupiedAreaTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Should_Set_Properties_When_Constructed()
        {
            Ship ship = _fixture.Create<Ship>();
            IEnumerable<ICell> cells = _fixture.CreateMany<ICell>(10);

            OccupiedArea area = new(ship, cells);

            area.Ship.Should().Be(ship);
            area.Cells.Should().BeEquivalentTo(cells);
        }

        [Fact]
        public void Should_Occupy_Passed_Cells()
        {
            Ship ship = _fixture.Create<Ship>();


            var cells = Enumerable.Range(0, new Random().Next(5)).Select(i =>
            {
                var cell = new Mock<ICell>(MockBehavior.Strict);
                cell.Setup(c => c.TryOccupy(It.Is<Ship>(s => s == ship)))
                                .Returns(true);
                return cell;
            }).ToList();

            OccupiedArea area = new(ship, cells.Select(cell=>cell.Object));
            
            // No need to any further assertion. Because ICell mock was set up as strict, if the setup is not called the test failed. 
        }
    }
}

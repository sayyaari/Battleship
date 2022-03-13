using Battleship.Model;
using Battleship.Services;
using AutoFixture;
using System.Linq;
using AutoFixture.AutoMoq;
using Xunit;
using FluentAssertions;

namespace Battleship.Tests.Services
{
    public class PositionGeneratorTests
    {
        private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        [Theory]
        [InlineData(Direction.Horizaontal)]
        [InlineData(Direction.Vertical)]
        public void Should_Next_Position_X_And_Y_Be_Incremented_With_Regards_To_IncrementSteps(Direction direction)
        {
            IncrementSteps incrementSteps = new(direction);
            var currentPosition = _fixture.Create<Position>();


            var nextPosition = new PositionGenerator().NextPosition(currentPosition, incrementSteps);


            nextPosition.Should().NotBe(currentPosition);
            nextPosition.X.Should().Be(currentPosition.X + incrementSteps.X);
            nextPosition.Y.Should().Be(currentPosition.Y + incrementSteps.Y);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(20)]
        [InlineData(340)]
        public void Should_Generate_Correct_Number_Of_Position(int numberOfPosition)
        {
            var currentPosition = _fixture.Create<Position>();
            PositionGenerator positionGenerator = new PositionGenerator();


            var positions = positionGenerator.Generate(currentPosition, _fixture.Create<Direction>(), new ShipSize(numberOfPosition)).ToList();


            positions.Should().HaveCount(numberOfPosition);
        }


        [Theory]
        [InlineData(Direction.Horizaontal)]
        [InlineData(Direction.Vertical)]
        public void Should_Generate_Correct_Serie_Of_Positions(Direction direction)
        {
            int numberOsPosition = 20;
            var currentPosition = _fixture.Create<Position>();
            PositionGenerator positionGenerator = new ();


            
            var positions = positionGenerator.Generate(currentPosition, direction, new ShipSize(numberOsPosition)).ToList();




            Position? prevPosition = null;
            positions.ToList().ForEach(p =>
            {
                if (prevPosition == null)
                {
                    p.Should().Be(currentPosition);

                }
                else
                {
                    var expectedPosition = positionGenerator.NextPosition(prevPosition.Value, new IncrementSteps(direction));
                    p.Should().NotBe(prevPosition);
                    p.Should().Be(expectedPosition);
                }
                prevPosition = p;
            });
        }

    }
}

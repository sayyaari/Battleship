using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class IncrementStepsTest
    {
        [Fact]
        public void Should_Step_X_Be_1_And_Y_Be_0_When_Direction_Iz_Horizontal()
        {
            IncrementSteps incrementSteps = new (Direction.Horizaontal);
            incrementSteps.X.Should().Be(1);
            incrementSteps.Y.Should().Be(0);
        }


        [Fact]
        public void Should_Step_X_Be_0_And_Y_1_Zero_When_Direction_Iz_Vertical()
        {
            IncrementSteps incrementSteps = new(Direction.Vertical);
            incrementSteps.X.Should().Be(0);
            incrementSteps.Y.Should().Be(1);
        }

    }
}

using Battleship.Model;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Model
{
    public class ShipSizeTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]

        public void Should_Length_Has_Correct_Value(int length)
        {
            ShipSize size = new(length);

            size.Length.Should().Be(length);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]

        public void Should_Length_Has_Correct_Value_When_Using_Operato_Overloading(int length)
        {
            ShipSize size = length;

            size.Length.Should().Be(length);
        }

    }
}

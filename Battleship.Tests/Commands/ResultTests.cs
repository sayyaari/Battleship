using Battleship.Commands;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests.Commands
{
    public class ResultTests
    {
        [Fact]
        public void Should_Error_Be_Null()
        {
            Result result = new Result();

            result.Error.Should().BeNull();

        }

        [Fact]
        public void Should_Be_Success_When_Does_Not_Have_Any_Error()
        {
            Result result = new Result();

            result.IsFailure().Should().BeFalse(); 
        }
    }
}
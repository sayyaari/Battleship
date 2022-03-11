using Battleship.Commands;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests
{
    public class CommandResultTests
    {
        [Fact]
        public void Should_Error_Be_Null()
        {
            CommandResult result = new CommandResult();

            result.Error.Should().BeNull();

        }

        [Fact]
        public void Should_Be_Success_When_Does_Not_Have_Any_Error()
        {
            CommandResult result = new CommandResult();

            result.IsFailure().Should().BeFalse(); 
        }
    }
}
using Battleship.Commands;

namespace Battleship.CommandHandlers
{
    public static class CommandResultExtensions
    {
        public static CommandResult<T> ToCommandResult<T>(this T result)
        {
            return new CommandResult<T>(result);
        }
    }
}

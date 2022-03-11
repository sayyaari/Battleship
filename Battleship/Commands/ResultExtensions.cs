namespace Battleship.Commands
{
    public static class ResultExtensions
    {
        public static Result<T> ToCommandResult<T>(this T result)
        {
            return new Result<T>(result);
        }
    }
}

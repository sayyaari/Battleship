namespace Battleship.Commands
{
    public class CommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(Error error)
        {
            Error = error;
        }

        public Error? Error { get; }
        
        public bool IsFailure() => Error != null;

        public static CommandResult Succeeded() => new();

        public static CommandResult Failed(Error error) => new(error);
    }

    public class CommandResult<T> : CommandResult
    {
        public CommandResult(Error error) : base(error)
        {
        }

        public CommandResult(T result) 
        {
            Result = result;
        }

        public T? Result { get; set; }

        public static CommandResult<T> Succeeded(T result) => new(result);
        
        public static new CommandResult<T> Failed(Error error) => new(error);
    }
}

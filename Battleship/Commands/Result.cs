namespace Battleship.Commands
{
    public class Result
    {
        public Result()
        {
        }

        public Result(Error error)
        {
            Error = error;
        }

        public Error? Error { get; }
        
        public bool IsFailure() => Error != null;

        public static Result Succeeded() => new();

        public static Result Failed(Error error) => new(error);
    }

    public class Result<T> : Result
    {
        public Result(Error error) : base(error)
        {
        }

        public Result(T value) 
        {
            Value = value;
        }

        public T? Value { get; set; }

        public static Result<T> Succeeded(T result) => new(result);
        
        public static new Result<T> Failed(Error error) => new(error);
    }
}

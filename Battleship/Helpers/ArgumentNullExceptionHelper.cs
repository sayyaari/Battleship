using System.Runtime.CompilerServices;

namespace Battleship.Helpers
{
    public static class ArgumentNullExceptionHelper
    {
        public static T ThrowIfNull<T>(this T argument, [CallerArgumentExpression("argument")] string? paramName = null) where T : class?
        {
            return argument ?? throw new ArgumentNullException(paramName);
        }

    }
}

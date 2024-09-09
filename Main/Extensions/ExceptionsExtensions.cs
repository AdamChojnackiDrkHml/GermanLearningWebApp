using CSharpFunctionalExtensions;

namespace TestWebApp.Extensions;

public static class ExceptionsExtensions
{
    public static Result<T> WrapThrowable<T>(Func<T> func)
    {
        try
        {
            return Result.Success(func());
        }
        catch (Exception e)
        {
            return Result.Failure<T>($"An exception {e.GetType()} occurred: {e.Message}");
        }
    }
}
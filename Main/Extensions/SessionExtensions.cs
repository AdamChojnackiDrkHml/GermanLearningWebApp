using System.Text.Json;
using CSharpFunctionalExtensions;
using static TestWebApp.Extensions.ExceptionsExtensions;

namespace TestWebApp.Extensions;

public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static Result<T> GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        
        if (value is null)
            return Result.Failure<T>($"Session does not contain key '{key}'");


        return WrapThrowable(() => JsonSerializer.Deserialize<T>(value))
            .Match(
                onFailure: error => Result.Failure<T>($"Failed to deserialize object from session: {error}"),
                onSuccess: obj => obj is not null 
                    ? obj 
                    : Result.Failure<T>($"Failed to deserialize object from session: {value}")
            );
    }
}
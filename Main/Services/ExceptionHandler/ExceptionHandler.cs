using Microsoft.AspNetCore.Diagnostics;

namespace TestWebApp.Services.ExceptionHandler;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger;
    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        this.logger = logger;
    }
    
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken
    )
    {
        var exceptionMessage = exception.Message;
        logger.LogError(
        "Error Message: {exceptionMessage}, Time of occurrence {time}",
        exceptionMessage, DateTime.UtcNow);
        
        return ValueTask.FromResult(false);
    }
}
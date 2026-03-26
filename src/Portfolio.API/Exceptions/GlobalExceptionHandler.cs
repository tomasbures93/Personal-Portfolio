using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Exceptions;

namespace Portfolio.API.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    // TODO: need to add logger
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var path = httpContext.Request.Path.ToString();

        var problemDetails = exception switch
        {
            DomainException ex => HandleDomainException(path, ex, cancellationToken),
            _ => HandleUnexpected(path, exception, cancellationToken)
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails HandleDomainException(string path, DomainException exception,  CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = "An error occurred.",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest,
            Instance = path
        };
        return problemDetails;
    }

    private ProblemDetails HandleUnexpected(string path, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = "Something went horribly wrong.",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = path
        };
        return problemDetails;
    }
}

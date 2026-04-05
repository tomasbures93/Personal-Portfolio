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
            DomainException ex => HandleDomainException(path, ex),
            _ => HandleUnexpected(path, exception)
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails HandleDomainException(string path, DomainException exception)
    {
        var problemDetails = new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-status-codes",
            Title = "An error occurred in Domain Layer.",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest,
            Instance = path
        };
        return problemDetails;
    }

    private ProblemDetails HandleUnexpected(string path, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-status-codes",
            Title = "Something went horribly wrong.",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = path
        };
        return problemDetails;
    }
}

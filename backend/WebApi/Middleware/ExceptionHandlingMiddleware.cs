using System.Net;
using ApplicationCore.Exceptions;

namespace WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (UserAlreadyExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (IncorrectPasswordException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}
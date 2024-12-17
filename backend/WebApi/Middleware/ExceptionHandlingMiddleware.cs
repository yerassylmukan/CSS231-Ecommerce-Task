using System.Net;
using ApplicationCore.Exceptions;

namespace WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

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
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (InvalidPasswordException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (RoleDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogBrandDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogTypeDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogItemDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogBrandAlreadyExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogItemReviewAlreadyExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogItemReviewDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogTypeAlreadyExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (WishlistDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (WishlistItemDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (OutOfStockException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CatalogItemAlreadyInWishlistException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CartDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (CartItemDoesNotExistsException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
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
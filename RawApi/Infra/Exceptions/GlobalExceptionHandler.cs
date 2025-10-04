using Microsoft.AspNetCore.Mvc;

namespace RawApi.Infra.Exceptions
{
    internal sealed class GlobalExceptionHandler(RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred!");

                context.Response.StatusCode = ex switch
                {
                    ApplicationException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Type = ex.GetType().Name,
                        Title = "An error occurred",
                        Detail = ex.Message,
                    });
            }
        }
    }
}

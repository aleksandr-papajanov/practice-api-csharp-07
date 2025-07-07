using Microsoft.AspNetCore.Http.HttpResults;
using PracticeApiCSharp07.DTOs.Mappers;
using PracticeApiCSharp07.Helpers;
using System.Text.Json;

namespace PracticeApiCSharp07.Middleware
{
    internal class ExceptionHandlingMiddleware
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
            httpContext.Response.ContentType = "application/json";

            try
            {
                await _next(httpContext);
            }
            catch (NotFoundAppException ex)
            {
                await WriteErrorResponseAsync(httpContext, ex);
            }
            catch (BadRequestAppException ex)
            {
                await WriteErrorResponseAsync(httpContext, ex);
            }
            catch (ValidationAppException ex)
            {
                await WriteErrorResponseAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                await WriteErrorResponseAsync(httpContext, new AppExceptionBase());
            }
        }

        private async Task WriteErrorResponseAsync(HttpContext context, AppExceptionBase ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(ex.ToDTO());

            await context.Response.WriteAsync(json);
        }
    }
}

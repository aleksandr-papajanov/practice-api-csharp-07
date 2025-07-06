using Microsoft.AspNetCore.Http.HttpResults;
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
            catch (KeyNotFoundException ex)
            {
                await WriteErrorResponseAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                await WriteErrorResponseAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await WriteErrorResponseAsync(httpContext, StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
            }
        }

        private async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var error = new { message };
            var json = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(json);
        }
    }
}

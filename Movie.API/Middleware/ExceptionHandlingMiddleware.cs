using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Movie.Services.Exceptions;
using Movie.Services.Mappers;
using System.Net.Http;
using System.Text.Json;

namespace Movie.API.Middleware
{
    internal class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";

            try
            {
                await _next(httpContext);
            }
            catch (ValidationAppException ex)
            {
                var problem = CreateProblem(ex, httpContext);
                problem.Extensions["errors"] = ex.Details; // Add validation errors to the problem details
                await WriteErrorResponseAsync(problem, httpContext);
            }
            catch (AppExceptionBase ex)
            {
                var problem = CreateProblem(ex, httpContext);
                await WriteErrorResponseAsync(problem, httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                var problem = _problemDetailsFactory.CreateProblemDetails(
                    httpContext: httpContext,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error",
                    detail: "An unexpected error occurred.",
                    instance: httpContext.Request.Path);

                await WriteErrorResponseAsync(problem, httpContext);
            }
        }

        private async Task WriteErrorResponseAsync(ProblemDetails problem, HttpContext context)
        {
            context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problem);
        }

        private ProblemDetails CreateProblem(AppExceptionBase ex, HttpContext context)
        {
            return _problemDetailsFactory.CreateProblemDetails(
                httpContext: context,
                statusCode: ex.StatusCode,
                title: ex.Title,
                detail: ex.Message,
                instance: context.Request.Path);
        }
    }
}

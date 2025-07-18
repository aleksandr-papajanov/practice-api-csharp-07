#pragma warning disable CS1591

using Microsoft.AspNetCore.Http;

namespace Movie.Core.Exceptions.BadRequest
{
    public abstract class BadRequestAppException : AppExceptionBase
    {
        public BadRequestAppException(string message = "The request is invalid.") : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            Title = "Bad Request";
        }
    }
}

#pragma warning disable CS1591

using Microsoft.AspNetCore.Http;

namespace Movie.Core.Exceptions.NotFound
{
    public abstract class NotFoundAppException : AppExceptionBase
    {
        public NotFoundAppException(string message = "The requested resource was not found.") : base(message)
        {
            StatusCode = StatusCodes.Status404NotFound;
            Title = "Resource Not Found";
        }
    }
}

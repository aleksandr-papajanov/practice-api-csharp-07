#pragma warning disable CS1591

using Microsoft.AspNetCore.Http;

namespace Movie.Core.Exceptions.Conflict
{
    public abstract class ConflictAppException : AppExceptionBase
    {
        public ConflictAppException(string message = "A conflict occurred with the current state of the resource.") : base(message)
        {
            StatusCode = StatusCodes.Status409Conflict;
            Title = "Conflict";
        }
    }
}
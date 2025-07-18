#pragma warning disable CS1591

using Microsoft.AspNetCore.Http;

namespace Movie.Core.Exceptions
{
    public class ValidationAppException : AppExceptionBase
    {
        public ValidationAppException(Dictionary<string, List<string>> errors) : base("Validation failed for the request.")
        {
            Details = errors;
            StatusCode = StatusCodes.Status400BadRequest;
            Title = "Validation Error";
        }
    }
}

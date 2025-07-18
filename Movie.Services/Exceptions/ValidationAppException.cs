using Microsoft.AspNetCore.Http;

namespace Movie.Services.Exceptions
{
    public class ValidationAppException : AppExceptionBase
    {
        public ValidationAppException(Dictionary<string, ICollection<string>> errors) : base("Validation failed for the request.")
        {
            Details = errors;
            StatusCode = StatusCodes.Status400BadRequest;
            Title = "Validation Error";
        }
    }
}

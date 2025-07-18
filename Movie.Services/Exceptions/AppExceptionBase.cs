using Microsoft.AspNetCore.Http;

namespace Movie.Services.Exceptions
{
    public abstract class AppExceptionBase : Exception
    {
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        public string Title { get; set; } = "An error occurred";
        public Dictionary<string, ICollection<string>> Details { get; set; } = [];

        public AppExceptionBase(string message = "An unexpected error occurred. Please try again later.") : base(message)
        {
        }
    }
}

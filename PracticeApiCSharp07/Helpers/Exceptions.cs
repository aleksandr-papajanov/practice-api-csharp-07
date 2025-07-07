namespace PracticeApiCSharp07.Helpers
{
    internal class AppExceptionBase : Exception
    {
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        public Dictionary<string, List<string>> Details { get; set; } = [];

        public AppExceptionBase(string message = "An unexpected error occurred. Please try again later.") : base(message)
        {
        }
    }

    internal class NotFoundAppException : AppExceptionBase
    {
        public NotFoundAppException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status404NotFound;
        }
    }
    
    internal class BadRequestAppException : AppExceptionBase
    {
        public BadRequestAppException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    
    internal class ValidationAppException : AppExceptionBase
    {
        public ValidationAppException(Dictionary<string, List<string>> errors) : base("Error has occured while validating request")
        {
            Details = errors;
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}

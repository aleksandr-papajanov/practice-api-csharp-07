namespace PracticeApiCSharp07.DTOs
{
    /// <summary>
    /// Represents an exception that occurred during API processing.
    /// </summary>
    public class ExceptionDTO
    {
        /// <summary>
        /// The error message describing the exception.
        /// </summary>
        /// <example>An unexpected error occurred.</example>
        public required string Message { get; set; }

        /// <summary>
        /// The HTTP status code associated with the exception.
        /// </summary>
        /// <example>500</example>
        public int StatusCode { get; set; }

        /// <summary>
        /// Additional details about the exception, if any.
        /// </summary>
        public object? Details { get; set; }
    }
}

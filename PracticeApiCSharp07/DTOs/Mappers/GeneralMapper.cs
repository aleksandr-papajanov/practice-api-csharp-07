using PracticeApiCSharp07.Helpers;

namespace PracticeApiCSharp07.DTOs.Mappers
{
    internal static class GeneralMapper
    {
        public static ExceptionDTO ToDTO(this AppExceptionBase exception) => new ExceptionDTO
        {
            Message = exception.Message,
            StatusCode = exception.StatusCode,
            Details = exception.Details
        };
    }
}

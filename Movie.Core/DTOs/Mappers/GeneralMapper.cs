using Movie.Core.Exceptions;

namespace Movie.Core.DTOs.Mappers
{
    public static class GeneralMapper
    {
        public static ExceptionDTO ToDTO(this AppExceptionBase exception) => new ExceptionDTO
        {
            Message = exception.Message,
            StatusCode = exception.StatusCode,
            Details = exception.Details
        };
    }
}

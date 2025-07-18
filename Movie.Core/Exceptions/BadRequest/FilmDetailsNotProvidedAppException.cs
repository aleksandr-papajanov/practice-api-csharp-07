#pragma warning disable CS1591

namespace Movie.Core.Exceptions.BadRequest
{
    public class FilmDetailsNotProvidedAppException : BadRequestAppException
    {
        public FilmDetailsNotProvidedAppException() 
            : base("Details must be provided for this operation.")
        {
        }
    }
}

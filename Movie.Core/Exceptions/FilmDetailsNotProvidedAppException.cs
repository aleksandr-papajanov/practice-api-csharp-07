namespace Movie.Core.Exceptions
{
    public class FilmDetailsNotProvidedAppException : BadRequestAppException
    {
        public FilmDetailsNotProvidedAppException() 
            : base("Details must be provided for this operation.")
        {
        }
    }
}

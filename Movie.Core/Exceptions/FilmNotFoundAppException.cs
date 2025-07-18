namespace Movie.Core.Exceptions
{
    public class FilmNotFoundAppException : NotFoundAppException
    {
        public FilmNotFoundAppException(int id) : base($"Film with ID {id} not found.")
        {
        }
    }
}

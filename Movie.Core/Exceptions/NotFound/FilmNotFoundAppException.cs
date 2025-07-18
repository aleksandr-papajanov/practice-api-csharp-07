#pragma warning disable CS1591

namespace Movie.Core.Exceptions.NotFound
{
    public class FilmNotFoundAppException : NotFoundAppException
    {
        public FilmNotFoundAppException(int id) : base($"Film with ID {id} not found.")
        {
        }
    }
}

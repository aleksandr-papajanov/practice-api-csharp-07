#pragma warning disable CS1591

namespace Movie.Core.Exceptions.NotFound
{
    public class FilmGenreNotFoundAppException : NotFoundAppException
    {
        public FilmGenreNotFoundAppException(string genre) 
            : base($"The genre '{genre}' does not exist.")
        {
        }
    }
}

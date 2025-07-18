namespace Movie.Core.Exceptions
{
    public class FilmGenreNotExistsAppException : BadRequestAppException
    {
        public FilmGenreNotExistsAppException(string genre) 
            : base($"The genre '{genre}' does not exist.")
        {
        }
    }
}

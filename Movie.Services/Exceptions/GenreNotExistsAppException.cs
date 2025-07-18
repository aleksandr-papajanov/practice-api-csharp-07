namespace Movie.Services.Exceptions
{
    public class GenreNotExistsAppException : BadRequestAppException
    {
        public GenreNotExistsAppException(string genre) 
            : base($"The genre '{genre}' does not exist.")
        {
        }
    }
}

namespace Movie.Core.Exceptions
{
    public class FilmTitleConflictAppException : ConflictAppException
    {
        public FilmTitleConflictAppException(string filmTitle)
            : base($"Film with title '{filmTitle}' already exists.")
        {
        }
    }
}
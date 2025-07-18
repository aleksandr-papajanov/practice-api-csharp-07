#pragma warning disable CS1591

namespace Movie.Core.Exceptions.Conflict
{
    public class FilmTitleConflictAppException : ConflictAppException
    {
        public FilmTitleConflictAppException(string filmTitle)
            : base($"Film with title '{filmTitle}' already exists.")
        {
        }
    }
}
#pragma warning disable CS1591

namespace Movie.Core.Exceptions.Conflict
{
    public class DocumentaryFilmMaxActorsExceededAppException : ConflictAppException
    {
        public DocumentaryFilmMaxActorsExceededAppException(int filmId, int maxCount)
            : base($"Documentary film with ID {filmId} cannot have more than {maxCount} actors.")
        {
        }
    }
}
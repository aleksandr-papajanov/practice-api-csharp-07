namespace Movie.Core.Exceptions
{
    public class DocumentaryFilmMaxActorsExceededAppException : ConflictAppException
    {
        public DocumentaryFilmMaxActorsExceededAppException(int filmId, int maxCount)
            : base($"Documentary film with ID {filmId} cannot have more than {maxCount} actors.")
        {
        }
    }
}
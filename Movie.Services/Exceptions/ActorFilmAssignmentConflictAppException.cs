namespace Movie.Services.Exceptions
{
    public class ActorFilmAssignmentConflictAppException : ConflictAppException
    {
        public ActorFilmAssignmentConflictAppException(int actorId, int filmId)
            : base($"Actor with ID {actorId} is already assigned to film with ID {filmId}.")
        {
        }
    }
}
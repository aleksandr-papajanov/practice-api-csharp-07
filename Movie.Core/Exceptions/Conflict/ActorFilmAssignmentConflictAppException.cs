#pragma warning disable CS1591

namespace Movie.Core.Exceptions.Conflict
{
    public class ActorFilmAssignmentConflictAppException : ConflictAppException
    {
        public ActorFilmAssignmentConflictAppException(int actorId, int filmId)
            : base($"Actor with ID {actorId} is already assigned to film with ID {filmId}.")
        {
        }
    }
}
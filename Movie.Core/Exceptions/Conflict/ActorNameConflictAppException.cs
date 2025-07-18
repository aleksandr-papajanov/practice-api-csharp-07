#pragma warning disable CS1591

namespace Movie.Core.Exceptions.Conflict
{
    public class ActorNameConflictAppException : ConflictAppException
    {
        public ActorNameConflictAppException(string actorName)
            : base($"Actor with name '{actorName}' already exists.")
        {
        }
    }
}
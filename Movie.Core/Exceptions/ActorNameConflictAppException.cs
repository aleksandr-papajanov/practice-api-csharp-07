namespace Movie.Core.Exceptions
{
    public class  ActorNameConflictAppException : ConflictAppException
    {
        public ActorNameConflictAppException(string actorName)
            : base($"Actor with name '{actorName}' already exists.")
        {
        }
    }
}
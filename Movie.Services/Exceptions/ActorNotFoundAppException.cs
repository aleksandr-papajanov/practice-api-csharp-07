namespace Movie.Services.Exceptions
{
    public class ActorNotFoundAppException : NotFoundAppException
    {
        public ActorNotFoundAppException(int id) : base($"Actor with ID {id} not found.")
        {
        }
    }
}

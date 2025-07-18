#pragma warning disable CS1591

namespace Movie.Core.Exceptions.NotFound
{
    public class ActorNotFoundAppException : NotFoundAppException
    {
        public ActorNotFoundAppException(int id) : base($"Actor with ID {id} not found.")
        {
        }
    }
}

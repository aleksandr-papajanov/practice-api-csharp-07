#pragma warning disable CS1591

namespace Movie.Core.Exceptions.NotFound
{
    public class ReviewNotFoundAppException : NotFoundAppException
    {
        public ReviewNotFoundAppException(int id) : base($"Review with ID {id} not found.")
        {
        }
    }
}

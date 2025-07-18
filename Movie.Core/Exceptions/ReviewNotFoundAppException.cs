namespace Movie.Core.Exceptions
{
    public class ReviewNotFoundAppException : NotFoundAppException
    {
        public ReviewNotFoundAppException(int id) : base($"Review with ID {id} not found.")
        {
        }
    }
}

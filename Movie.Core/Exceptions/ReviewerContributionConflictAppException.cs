namespace Movie.Core.Exceptions
{
    public class ReviewerContributionConflictAppException : ConflictAppException
    {
        public ReviewerContributionConflictAppException(string reviewer, int filmId)
            : base($"Reviewer {reviewer} has already contributed to film with ID {filmId}.")
        {
        }
    }
}
namespace Movie.Core.Exceptions
{
    public class PatchDocumentNullAppException : BadRequestAppException
    {
        public PatchDocumentNullAppException() : base("The patch document cannot be null.")
        {
        }
    }
}

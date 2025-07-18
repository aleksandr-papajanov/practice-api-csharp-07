#pragma warning disable CS1591

namespace Movie.Core.Exceptions.BadRequest
{
    public class PatchDocumentNullAppException : BadRequestAppException
    {
        public PatchDocumentNullAppException() : base("The patch document cannot be null.")
        {
        }
    }
}

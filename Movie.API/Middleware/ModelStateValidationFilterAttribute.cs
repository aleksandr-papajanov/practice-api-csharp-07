using Microsoft.AspNetCore.Mvc.Filters;
using Movie.API.Helpers;
using Movie.Core.Exceptions;

namespace Movie.API.Middleware
{
    internal class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = ValidationHelper.GetErrors(context.ModelState);
                throw new ValidationAppException(errors);
            }
        }
    }
}

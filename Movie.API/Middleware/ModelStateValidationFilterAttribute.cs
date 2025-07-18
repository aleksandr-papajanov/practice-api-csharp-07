using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movie.Services.Exceptions;

namespace Movie.API.Middleware
{
    internal class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = GetErrors(context.ModelState);
                throw new ValidationAppException(errors);
            }
        }

        private Dictionary<string, ICollection<string>> GetErrors(ModelStateDictionary modelState)
        {
            var collection = new Dictionary<string, ICollection<string>>();

            foreach (var state in modelState)
            {
                if (!state.Value.Errors.Any())
                {
                    continue;
                }

                collection.Add(
                    state.Key,
                    state.Value.Errors
                        .Select(x => x.ErrorMessage)
                        .ToList());

            }

            return collection;
        }
    }
}

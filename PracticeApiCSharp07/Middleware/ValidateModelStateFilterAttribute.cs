using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PracticeApiCSharp07.Helpers;

namespace ProductOrderApi.Middleware
{
    internal class ValidateModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = GetErrors(context.ModelState);
                throw new ValidationAppException(errors);
            }
        }

        private Dictionary<string, List<string>> GetErrors(ModelStateDictionary modelState)
        {
            var collection = new Dictionary<string, List<string>>();

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

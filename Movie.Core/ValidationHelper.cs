using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movie.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Movie.API.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateDto(object dto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(dto);

            bool isValid = Validator.TryValidateObject(dto, context, validationResults, true);

            if (!isValid)
            {
                var errors = validationResults
                    .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(r => r.ErrorMessage ?? "Validation error").ToList());

                throw new ValidationAppException(errors);
            }
        }

        public static Dictionary<string, List<string>> GetErrors(ModelStateDictionary modelState)
        {
            var collection = new Dictionary<string, List<string>>();

            foreach (var state in modelState)
            {
                if (!state.Value.Errors.Any())
                    continue;

                collection.Add(
                    state.Key,
                    state.Value.Errors.Select(x => x.ErrorMessage).ToList());
            }

            return collection;
        }
    }
}

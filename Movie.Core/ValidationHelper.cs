#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movie.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Movie.Core
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
            var collection = modelState
                .Where(state => state.Value.Errors.Any())
                .ToDictionary(
                    state => state.Key,
                    state => state.Value.Errors.Select(x => x.ErrorMessage).ToList()
                );

            return collection;
        }
    }
}

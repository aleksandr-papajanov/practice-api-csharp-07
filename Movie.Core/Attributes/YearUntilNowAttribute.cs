using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Attributes
{
    internal class YearUntilNowAttribute(int minYear) : ValidationAttribute
    {
        private readonly int _minYear = minYear;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int currentYear = DateTime.UtcNow.Year;

                if (year < _minYear || year > currentYear)
                {
                    return new ValidationResult($"Year must be between {_minYear} and {currentYear}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PracticeApiCSharp07.Helpers
{
    internal class YearUntilNowAttribute : ValidationAttribute
    {
        private readonly int _minYear;

        public YearUntilNowAttribute(int minYear)
        {
            _minYear = minYear;
        }

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

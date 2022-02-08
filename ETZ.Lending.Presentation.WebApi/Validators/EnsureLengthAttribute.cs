using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ETZ.Lending.Presentation.WebApi.Validators
{
    public class EnsureLengthAttribute : ValidationAttribute
    {
        private readonly int _minimum;
        private readonly int _maximum;

        public EnsureLengthAttribute(int length)
        {
            _minimum = length;
            _maximum = length;

            ErrorMessage = "{0} length should be {1}";
        }

        public EnsureLengthAttribute(int minimum, int maximum)
        {
            _minimum = minimum;
            _maximum = maximum;

            ErrorMessage = "{0} length should be between {1} and {2}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var length = Convert.ToString(value)!.Length;
            if (length < _minimum || length > _maximum)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _minimum, _maximum);
        }
    }
}
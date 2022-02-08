using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ETZ.Lending.Presentation.WebApi.Validators
{
    public class EnsureMinimumElementCountAttribute : ValidationAttribute
    {
        private readonly int _minimum;

        public EnsureMinimumElementCountAttribute(int minimum)
        {
            _minimum = minimum;
        }

        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count >= _minimum;
            }

            return false;
        }
    }
}
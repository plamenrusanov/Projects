using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ValidationAttributes
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int age;

        public MinAgeAttribute(int age)
        {
            this.age = age;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (!(value is DateTime) || !(value is DateTime?)) // Nullable<DateTime>
            {
                return new ValidationResult("Invalid DateTime object.");
            }
            var dateTime = (DateTime)value;
            if (DateTime.UtcNow > dateTime.AddYears(age))
            {
                return new ValidationResult(this.ErrorMessage.Replace("{0}", dateTime.ToString()));
            }
            return ValidationResult.Success;
        }
    }
}

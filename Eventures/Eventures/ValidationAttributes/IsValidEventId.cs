using Eventures.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ValidationAttributes
{
    public class IsValidEventId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var eventService = (IEventService)validationContext.GetService(typeof(IEventService));

            if (eventService.Exist(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid event id!");
            }
        }
    }
}

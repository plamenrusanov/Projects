using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
    public class CreateEventViewModel : IValidatableObject
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalTickets { get; set; }

        //[Range(typeof(decimal), "0", "1000", ErrorMessage = "{0} has to be between {1} and {2}")]
        [Required]
        public decimal PricePerTicket { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.End <= this.Start)
            {
                yield return new ValidationResult("The end date can not be before the start date");
            }

            if (String.IsNullOrEmpty(this.Place))
            {
                yield return new ValidationResult($"{nameof(this.Place)} can not be null or empty!");
            }
        }
    }
}

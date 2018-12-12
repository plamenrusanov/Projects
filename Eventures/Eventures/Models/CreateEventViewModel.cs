using Eventures.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class CreateEventViewModel : IValidatableObject
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TotalTickets { get; set; }

        [Range(typeof(decimal),"0", "1000", ErrorMessage ="{0} has to be between {1} and {2}")]
        public decimal PricePerTicket { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.End <= this.Start)
            {
                yield return new ValidationResult("The end date can not be before the start date");
            }
        }
    }
}

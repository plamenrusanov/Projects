using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
    public class BuyTicketViewModel : IValidatableObject
    {

        public string EventId { get; set; }

        public string Where { get; set; }

        public DateTime  When { get; set; }

        public int Available { get; set; }

        public decimal RegularPrice { get; set; }

        public IEnumerable<SelectListItem> Quantity => new List<SelectListItem>
        {
            new SelectListItem{ Value = "1" , Text= "1"},
            new SelectListItem{ Value = "2" , Text= "2"},
            new SelectListItem{ Value = "3" , Text= "3"},
            new SelectListItem{ Value = "4" , Text= "4"},
            new SelectListItem{ Value = "5" , Text= "5"},
            new SelectListItem{ Value = "6" , Text= "6"},
            new SelectListItem{ Value = "7" , Text= "7"},
         };

        [Range(0, 7)]
        public int AdultQuantity { get; set; }

        [Range(0, 7)]
        public int ChildQuantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Available < AdultQuantity + ChildQuantity)
            {
                yield return new ValidationResult($"Available have {Available} tickets.");
            }
        }
    }  
}


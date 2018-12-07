using Eventures.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class CreateEventViewModel
    {
        public string Name { get; set; }

        public string Place { get; set; }

        [MinAge(18, ErrorMessage ="Min Age = {0}")]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TotalTickets { get; set; }


        [Range(typeof(decimal),"0", "1000")]
        public decimal PricePerTicket { get; set; }
    }
}

using Eventures.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class EditEventViewModel
    {
        [Required]
        [IsValidEventId]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Range(0, 1000)]
        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }
    }
}

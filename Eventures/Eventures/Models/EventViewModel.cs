using Eventures.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
    public class EventViewModel
    {
        [Required]
        [IsValidEventId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}

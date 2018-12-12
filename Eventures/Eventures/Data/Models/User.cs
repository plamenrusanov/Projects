using Eventures.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UniqueCitizenNumber { get; set; }

        [MinAge(18, ErrorMessage = "Min Age = {0}")]
        public DateTime? DayOfBirth { get; set; }

        //public IFormFile Image { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Eventures.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UniqueCitizenNumber { get; set; }

        public DateTime? DayOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}

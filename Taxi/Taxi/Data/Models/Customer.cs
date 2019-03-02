using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Customer : IdentityUser
    {
        public Customer(string firstname, string lastname) : base()
        {
            this.

        }
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}

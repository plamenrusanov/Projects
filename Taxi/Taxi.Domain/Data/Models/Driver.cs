using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }

        public string CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}

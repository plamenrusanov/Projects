using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taxi.Domain.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}

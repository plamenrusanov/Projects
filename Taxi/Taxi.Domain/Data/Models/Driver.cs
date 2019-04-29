using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taxi.Domain.Data.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }

        [ForeignKey("CarId")]
        public string CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}

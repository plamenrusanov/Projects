using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taxi.Domain.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transfers = new HashSet<Transfer>();
        }
        public string Id { get; set; }

        [ForeignKey("DriverId")]
        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
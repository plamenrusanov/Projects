using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Car
    {
        public string Id { get; set; }

        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
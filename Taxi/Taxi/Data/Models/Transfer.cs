using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Transfer
    {
        public string Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Price { get; set; }

        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public string CarId { get; set; }
        public virtual Car Car { get; set; }


    }
}

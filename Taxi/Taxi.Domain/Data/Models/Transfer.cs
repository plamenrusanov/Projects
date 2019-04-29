using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taxi.Domain.Data.Models
{
    public class Transfer
    {
        public Transfer()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("DriverId")]
        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public  virtual Customer Customer { get; set; }

        [ForeignKey("CarId")]
        public string CarId { get; set; }
        public virtual Car Car { get; set; }


    }
}

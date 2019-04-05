using System;
using System.Collections.Generic;

namespace Taxi.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

    }
}
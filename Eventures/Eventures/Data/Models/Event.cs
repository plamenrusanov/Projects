using System;
using System.Collections.Generic;

namespace Eventures.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }

        public decimal Gainings { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

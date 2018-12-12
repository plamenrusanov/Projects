using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Data.Models
{
    public class Ticket
    {
        public string Id { get; set; }

        public string Area { get; set; }

        public int? Row { get; set; }

        public int? Number { get; set; }

        public decimal RegularPrice { get; set; }

        public string UserId { get; set; }
        public User Owner { get; set; }
    }
}

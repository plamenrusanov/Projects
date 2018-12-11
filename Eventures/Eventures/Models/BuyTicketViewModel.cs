using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class BuyTicketViewModel
    {
        public string EventId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }  
}


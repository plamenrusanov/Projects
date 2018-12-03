using Eventures.Data;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext context;

        public EventService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public string Create(CreateEventViewModel model)
        {
            var even = new Event()
            {
                Name = model.Name,
                Place = model.Place,
                PricePerTicket = model.PricePerTicket,
                Start = model.Start,
                End = model.End,
                TotalTickets = model.TotalTickets
            };
            this.context.Events.Add(even);
            this.context.SaveChanges();
            return null;
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.context.Events
                .Select(x => new EventViewModel() {
                    Name = x.Name,
                    Place = x.Place,
                    Start = x.Start,
                    End = x.End
                })
                .OrderBy(x => x.Start)
                .ToList();
        }
    }
}

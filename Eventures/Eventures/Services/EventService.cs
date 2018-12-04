using Eventures.Data;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return $"Successfully create event {model.Name}";
        }

        public string DeleteEvent(string id)
        {
            if (!Exist(id))
            {
                return null;
            }
            var ev = this.context.Events.Find(id);
            this.context.Events.Remove(ev);
            this.context.SaveChanges();
            return $"Successfully delete event {ev.Name}";
        }

        public string EditEvent(EditEventViewModel model)
        {
            var ev = this.context.Events.Find(model.Id);
            
            {
               ev.Name = model.Name;
               ev.Start = model.Start;
               ev.End = model.End;
               ev.Place = model.Place;
               ev.TotalTickets = model.TotalTickets;
               ev.PricePerTicket = model.PricePerTicket;
            }
            this.context.SaveChanges();
            return $"Successfully edit event {model.Name}";
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.context.Events
                .Select(x => new EventViewModel() {
                    Id = x.Id,
                    Name = x.Name,
                    Place = x.Place,
                    Start = x.Start,
                    End = x.End
                })
                .OrderBy(x => x.Start)
                .ToList();
        }

        public EditEventViewModel GetEvent(string id)
        {
            if (!Exist(id))
            {
                return null;
            }

            var ev = this.context.Events.Find(id);

            return new EditEventViewModel()
            {
                Id = ev.Id,
                Name = ev.Name,
                Start = ev.Start,
                End = ev.End,
                Place = ev.Place,
                TotalTickets = ev.TotalTickets,
                PricePerTicket = ev.PricePerTicket
            };
                
        }

        private bool Exist(string id)
        {
            return this.context.Events.Any(x => x.Id == id);
        }
    }
}

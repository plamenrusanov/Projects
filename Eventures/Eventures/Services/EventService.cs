using AutoMapper;
using Eventures.Data;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Eventures.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EventService(ApplicationDbContext context,
                            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string BuyTicets([FromForm]BuyTicketViewModel model )
        {
            var ev = this.context.Events.FirstOrDefault(x => x.Id == model.EventId);
            if (ev == null)
            {
                return "Тhe event does not exist";
            }

            var adultMoney = model.AdultQuantity * model.RegularPrice;
            var childMoney = model.ChildQuantity * (model.RegularPrice/2);
            ev.Gainings += (adultMoney + childMoney);

            var tickets = model.AdultQuantity + model.ChildQuantity;
            ev.TotalTickets -= tickets;
            this.context.SaveChanges();

            return $"Successfully bought {tickets} tickets.";
            
        }

        public string Create(CreateEventViewModel model)
        {
            var even = mapper.Map<Event>(model);          
            this.context.Events.Add(even);
            this.context.SaveChanges();
            return $"Successfully create event {model.Name}";
        }

        public BuyTicketViewModel CreateBuyTicketViewModel(string eventId)
        {
            if (!Exist(eventId))
            {
                return null;
            }
            var ev = this.context.Events.Find(eventId);            
            var mappedEvent = mapper.Map<BuyTicketViewModel>(ev);
            return mappedEvent;
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
            mapper.Map(model, ev);
            this.context.SaveChanges();
            return $"Successfully edit event {model.Name}";
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.context.Events
                .Select(x => mapper.Map<EventViewModel>(x))
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
            return mapper.Map<EditEventViewModel>(ev);          
        }

        private bool Exist(string id)
        {
            return this.context.Events.Any(x => x.Id == id);
        }
    }
}

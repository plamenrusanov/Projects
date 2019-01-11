using AutoMapper;
using Eventures.Data.Common;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Eventures.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Event> repository;

        public EventService(IMapper mapper,
                            IRepository<Event> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public string BuyTickets(BuyTicketViewModel model )
        {
            var ev = this.FindEventById(model.EventId);
            if (ev == null)
            {
                return "Тhe event does not exist";
            }

            var adultMoney = model.AdultQuantity * model.RegularPrice;
            var childMoney = model.ChildQuantity * (model.RegularPrice/2);
            ev.Gainings += (adultMoney + childMoney);

            var tickets = model.AdultQuantity + model.ChildQuantity;
            ev.TotalTickets -= tickets;
            this.repository.SaveChangesAsync();

            return $"Successfully bought {tickets} tickets.";
            
        }

        public string Create(CreateEventViewModel model)
        {
            var even = mapper.Map<Event>(model);          
            this.repository.AddAsync(even);
            this.repository.SaveChangesAsync();
            return $"Successfully create event {model.Name}";
        }

        public BuyTicketViewModel CreateBuyTicketViewModel(string eventId)
        {
            if (!Exist(eventId))
            {
                return null;
            }
            var ev = this.FindEventById(eventId);  
            var mappedEvent = mapper.Map<BuyTicketViewModel>(ev);
            return mappedEvent;
        }

        public string DeleteEvent(string id)
        {
            if (!Exist(id))
            {
                return null;
            }
            var ev = this.FindEventById(id);
            this.repository.Delete(ev);
            this.repository.SaveChangesAsync();
            return $"Successfully delete event {ev.Name}";
        }

        public string EditEvent(EditEventViewModel model)
        {
            var ev = this.FindEventById(model.Id);
            mapper.Map(model, ev);
            this.repository.SaveChangesAsync();
            return $"Successfully edit event {model.Name}";
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.repository.All()
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
            var ev = this.FindEventById(id);
            return mapper.Map<EditEventViewModel>(ev);          
        }

        public bool Exist(string id)
        {
            return this.repository.All().Any(x => x.Id == id);
        }

        private Event FindEventById(string id)
        {
            return this.repository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}

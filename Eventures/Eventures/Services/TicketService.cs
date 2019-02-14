using AutoMapper;
using Eventures.Data.Common;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public TicketService(IRepository<Event> eventRepository,
                             IRepository<Ticket> ticketRepository,
                             IEventService eventService,
                             IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.ticketRepository = ticketRepository;
            this.eventService = eventService;
            this.mapper = mapper;
        }
        public string BuyTickets(BuyTicketViewModel model, string userId)
        {
            var ev = eventService.FindEventById(model.EventId);
            if (ev == null)
            {
                return "Тhe event does not exist";
            }
            CreateTicket(model, userId);
            var adultMoney = model.AdultQuantity * model.RegularPrice;
            var childMoney = model.ChildQuantity * (model.RegularPrice / 2);
            ev.Gainings += (adultMoney + childMoney);

            var tickets = model.AdultQuantity + model.ChildQuantity;
            ev.TotalTickets -= tickets;
            eventRepository.SaveChangesAsync();

            return $"Successfully bought {tickets} tickets.";

        }
        private void CreateTicket(BuyTicketViewModel model, string userId)
        {
            Ticket ticket = mapper.Map<BuyTicketViewModel, Ticket>(model);
            ticket.UserId = userId;
            ticketRepository.AddAsync(ticket);
            ticketRepository.SaveChangesAsync();
        }

        public BuyTicketViewModel CreateBuyTicketViewModel(string eventId)
        {
            if (!eventService.Exist(eventId))
            {
                return null;
            }
            var ev = eventService.FindEventById(eventId);
            var mappedEvent = mapper.Map<BuyTicketViewModel>(ev);
            return mappedEvent;
        }
    }
}

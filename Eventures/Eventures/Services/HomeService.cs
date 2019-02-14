using AutoMapper;
using Eventures.Data.Common;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventures.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IMapper mapper;

        public HomeService(IRepository<Ticket> ticketRepository,
                           IMapper mapper)
        {
            this.ticketRepository = ticketRepository;
            this.mapper = mapper;
        }

        public IEnumerable<MyEventViewModel> GetMyEvents(string userId)
        {

            var tickets = ticketRepository.All().Where(x => x.UserId == userId && x.Event.Start > DateTime.UtcNow).ToList();
            var myEvents = new List<MyEventViewModel>();
            foreach (var ticket in tickets?.OrderBy(x => x.Event.Start))
            {
                myEvents.Add(mapper.Map<Ticket, MyEventViewModel>(ticket));
            }
            return myEvents;
        }
    }
}

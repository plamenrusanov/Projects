using AutoMapper;
using Eventures.AutoMapper;
using Eventures.Cloud.Contracts;
using Eventures.Common;
using Eventures.Data;
using Eventures.Data.Common;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Eventures.Services.Tests
{
    public class EventServiceTests
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IMapper mapper;
        private readonly IEventService eventService;

        public EventServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyDbInMemory")
               .Options;
            this.context = new ApplicationDbContext(options);
            this.eventRepository = new DbRepository<Event>(context);
            this.ticketRepository = new DbRepository<Ticket>(context);
            var cloudService = new Mock<ICloudService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            this.mapper = mappingConfig.CreateMapper();

            this.eventService = new EventService(mapper, eventRepository, ticketRepository, cloudService.Object);
        }

        [Fact]
        public void CreateEventShouldRealyCreate()
        {
            var eventModel = new CreateEventViewModel
            {
                Name = "SomeName",
                Place = "SomePlace",
                TotalTickets = 100,
                PricePerTicket = 10.50m,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(3)
            };
            this.eventService.CreateEvent(eventModel);

            var actual = this.context.Events.FirstOrDefaultAsync(x => x.Name == eventModel.Name).Result;

            Assert.True(eventModel.Name == actual.Name);
            Assert.True(eventModel.Place == actual.Place);
            Assert.True(eventModel.TotalTickets == actual.TotalTickets);
            Assert.True(eventModel.PricePerTicket == actual.PricePerTicket);
            this.context.Events.Remove(actual);
            this.context.SaveChangesAsync();
        }

        [Fact]
        public void DeleteEventShouldRealyDelete()
        {
            var ev = new Event
            {
                Name = "SomeName",
                Place = "SomePlace",
                TotalTickets = 100,
                PricePerTicket = 10.50m,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(3)
            };

            this.context.Events.AddAsync(ev);
            this.context.SaveChangesAsync();

            this.eventService.DeleteEvent(ev.Id);

            var actual = this.context.Events.FirstOrDefaultAsync(x => x.Name == ev.Name).Result;

            Assert.True(actual == null);
        }

        [Fact]
        public void DeleteEventShouldReturnNotExistWhitFakeId()
        {
            var result = this.eventService.DeleteEvent("fakeId");
            Assert.True(result == GlobalConstants.EventNotExist);
        }
    }
}

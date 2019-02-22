using AutoMapper;
using Eventures.AutoMapper;
using Eventures.Cloud.Contracts;
using Eventures.Data;
using Eventures.Data.Models;
using Eventures.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Eventures.Services.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEventShouldRealyCreate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDbInMemory")
                .Options;

            var db = new ApplicationDbContext(options);
            var repository = new DbRepository<Event>(db);
            var cloudService = new Mock<ICloudService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            var eventService = new EventService(mapper, repository, null, cloudService.Object);
            var eventModel = new CreateEventViewModel
            {
                Name = "SomeName",
                Place = "SomePlace",
                TotalTickets = 100,
                PricePerTicket = 10.50m,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(3)
            };
            eventService.CreateEvent(eventModel);

            var actual = db.Events.FirstOrDefaultAsync(x => x.Name == eventModel.Name);

            Assert.True(eventModel.Name == actual.Result.Name);
        }
    }
}

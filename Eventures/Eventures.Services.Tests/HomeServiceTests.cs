using System;
using Xunit;
using Moq;
using Eventures.Data.Models;
using Eventures.Data.Common;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Eventures.Services.Tests
{
    public class HomeServiceTests
    {
        public IEnumerable<Ticket> GetTickets()
        {
            return new List<Ticket>
            {
                new Ticket {UserId = "2", Event = new Event{Start = DateTime.Now} },
                new Ticket {UserId = "1", Event = new Event{Start = DateTime.Now}  },
                new Ticket {UserId = "2", Event = new Event{Start = DateTime.Now}  },
                new Ticket {UserId = "2", Event = new Event{Start = DateTime.Now.AddDays(-1) }  }
            };
        }

        [Fact]
        public void GetMyEventsShouldReturnsCorrectCount()
        {
            var repo = new Mock<IRepository<Ticket>>();
            repo.Setup(r => r.All())
                .Returns(GetTickets().AsQueryable());
            var mapper = new Mock<IMapper>();
            var service = new HomeService(repo.Object, mapper.Object);

            var actual = service.GetMyEvents("2").Count();
            var expected = 2;

            Assert.Equal(expected, actual);
        }
    }
}

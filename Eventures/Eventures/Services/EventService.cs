using AutoMapper;
using Eventures.Cloud.Contracts;
using Eventures.Common;
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
    public class EventService : IEventService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Event> repository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly ICloudService cloudService;

        public EventService(IMapper mapper,
                            IRepository<Event> repository, 
                            IRepository<Ticket> ticketRepository,
                            ICloudService cloudService)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.ticketRepository = ticketRepository;
            this.cloudService = cloudService;
        }
      
        public string CreateEvent(CreateEventViewModel model)
        {
            var even = mapper.Map<Event>(model);
            if (model.Image != null)
            {
                even.ImageUrl = cloudService.UploadImageToCloud(model.Image).Result;
            }
            this.repository.AddAsync(even);
            this.repository.SaveChangesAsync();
            return string.Format(GlobalConstants.CreateEvent, model.Name);
        }

        public string DeleteEvent(string id)
        {
            if (!Exist(id))
            {
                return GlobalConstants.EventNotExist;
            }
            var ev = this.FindEventById(id);
            this.repository.Delete(ev);
            this.repository.SaveChangesAsync();
            return string.Format(GlobalConstants.DeleteEvent, ev.Name);
        }

        public string EditEvent(EditEventViewModel model)
        {
            var ev = this.FindEventById(model.Id);
            mapper.Map(model, ev);
            this.repository.SaveChangesAsync();
            return string.Format(GlobalConstants.EditEvent, model.Name);
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.repository.All()
                .Where(x => x.Start > DateTime.Now)
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

        public Event FindEventById(string id)
        {
            return this.repository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}

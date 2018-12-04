using Eventures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Services.Contracts
{
    public interface IEventService
    {
        string Create(CreateEventViewModel model);

        IEnumerable<EventViewModel> GetAllEvents();

        EditEventViewModel GetEvent(string id);

        string EditEvent(EditEventViewModel model);

        string DeleteEvent(string id);
    }
}

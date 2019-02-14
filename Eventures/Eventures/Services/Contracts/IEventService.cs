using Eventures.Models;
using System.Collections.Generic;

namespace Eventures.Services.Contracts
{
    public interface IEventService
    {
        string Create(CreateEventViewModel model);

        IEnumerable<EventViewModel> GetAllEvents();

        EditEventViewModel GetEvent(string id);

        string EditEvent(EditEventViewModel model);

        string DeleteEvent(string id);

        string BuyTickets(BuyTicketViewModel model, string userId);

        BuyTicketViewModel CreateBuyTicketViewModel(string eventId);

        bool Exist(string id);
    }
}

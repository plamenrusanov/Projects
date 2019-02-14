using Eventures.Models;

namespace Eventures.Services.Contracts
{
    public interface ITicketService
    {
        string BuyTickets(BuyTicketViewModel model, string userId);

        BuyTicketViewModel CreateBuyTicketViewModel(string eventId);
    }
}
using Eventures.Models;
using System.Collections.Generic;

namespace Eventures.Services.Contracts
{
    public interface IHomeService
    {
        IEnumerable<MyEventViewModel> GetMyEvents(string userId);
    }
}

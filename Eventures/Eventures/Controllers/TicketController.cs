using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ValidationAttributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITicketService ticketService;

        public TicketController(UserManager<User> userManager,
                                ITicketService ticketService )
        {
            this.userManager = userManager;
            this.ticketService = ticketService;
        }

        [HttpGet]
        public IActionResult BuyTicket([IsValidEventId]string eventId)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            var model = this.ticketService.CreateBuyTicketViewModel(eventId);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult BuyTicket([FromForm]BuyTicketViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var userId = this.userManager.GetUserId(this.User);
            string result = this.ticketService.BuyTickets(model, userId);
            return Redirect("/Event/AllEvents");
        }

        public int GetAvailable(string child, string adult, string available)
        {
            var quantity = int.Parse(child) + int.Parse(adult);
            var av = int.Parse(available) - quantity;
            return av;
        }
    }
}

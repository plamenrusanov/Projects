using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger<EventController> logger;
        private readonly UserManager<User> userManager;

        public EventController(IEventService eventService,
                               ILogger<EventController> logger, 
                               UserManager<User> userManager)
        {
            this.eventService = eventService;
            this.logger = logger;
            this.userManager = userManager;
        }

        public IActionResult AllEvents()
        {
            IEnumerable<EventViewModel> models = this.eventService.GetAllEvents();
            return this.View(models);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateEventViewModel model )
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(User);
            if (!await this.userManager.IsInRoleAsync(user, "Admin"))
            {
                return this.Unauthorized();
            }
            this.eventService.Create(model);
            this.logger.LogInformation("Event created: " + model.Name, model);
            return Redirect("AllEvents");
        }

        [HttpGet]
        public IActionResult Edit([IsValidEventId]string id)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            var model = eventService.GetEvent(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit([FromForm]EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);               
            }
            eventService.EditEvent(model);
            return RedirectToAction("AllEvents");
        }

        public IActionResult Details([IsValidEventId]string id)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            var model = eventService.GetEvent(id);
            return this.View(model);
        }


        public IActionResult Delete([IsValidEventId]string id)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            var model = eventService.GetEvent(id);
            return this.View(model);
        }


        public IActionResult OnDelete([IsValidEventId]string id)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            string result = eventService.DeleteEvent(id);
            return Redirect("/Event/AllEvents");
        }

        [HttpGet]
        public IActionResult BuyTicket([IsValidEventId]string eventId)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("AllEvents");
            }
            var model = this.eventService.CreateBuyTicketViewModel(eventId);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult BuyTicket([FromForm]BuyTicketViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }    
            string result = eventService.BuyTickets(model);
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

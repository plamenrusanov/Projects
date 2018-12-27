using AutoMapper;
using Eventures.Data.Models;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(
            [FromForm]
            CreateEventViewModel model,
            [FromHeader]
            string connection,
            [FromServices]
            ILogger<EventController> logger,
           // [ModelBinder(typeof())]
            string allMyHeaders
            )
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
        public IActionResult Edit(string id)
        {
            var model = eventService.GetEvent(id);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit([FromForm]EditEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                eventService.EditEvent(model);
                return Redirect("/Event/AllEvents");
            }
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = eventService.GetEvent(id);

            return this.View(model);
        }


        public IActionResult Delete(string id)
        {
            var model = eventService.GetEvent(id);

            return this.View(model);
        }


        public IActionResult OnDelete(string id)
        {
            string result = eventService.DeleteEvent(id);

            return Redirect("/Event/AllEvents");
        }

        [HttpGet]
        public IActionResult BuyTicket(string eventId)
        {
            var model = this.eventService.CreateBuyTicketViewModel(eventId);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult BuyTicket(BuyTicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string result = eventService.BuyTicets(model);

            return Redirect("/Event/AllEvents");
        }


    }
}

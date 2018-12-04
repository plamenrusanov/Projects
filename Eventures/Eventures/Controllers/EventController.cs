using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger<EventController> logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            this.eventService = eventService;
            this.logger = logger;
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
        public IActionResult Create(CreateEventViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.eventService.Create(model);
                this.logger.LogInformation("Event created: " + model.Name, model);
                return Redirect("AllEvents");
            }
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = eventService.GetEvent(id);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditEventViewModel model)
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


    }
}

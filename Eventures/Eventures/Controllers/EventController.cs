using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
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
                return Redirect("/");
            }
            return this.View(model);
        }


    }
}

using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eventures.Models;
using Eventures.Data.Models;
using Eventures.Data.Common;

namespace Eventures.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Event> repository;

        public HomeController(IRepository<Event> repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = $"My application has {this.repository.All().Count()} events.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

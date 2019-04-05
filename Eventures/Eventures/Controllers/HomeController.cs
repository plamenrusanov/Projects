using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Eventures.Models;
using Eventures.Data.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly UserManager<User> userManager;

        public HomeController(IHomeService homeService,
                              UserManager<User> userManager)
        {
            this.homeService = homeService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var myEvents = this.homeService.GetMyEvents(userId);
            return View(myEvents);
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

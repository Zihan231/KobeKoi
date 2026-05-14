using KobeKoi.BLL.Service.Events;
using KobeKoi.DAL.EF;
using KobeKoi.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KobeKoi.web.Controllers
{
    public class HomeController : Controller
    {
        EventService eventService;

        public HomeController (EventService eventService)
        {
            this.eventService = eventService;
        }



        public IActionResult Index()
        {
            var events = eventService.GetAllEvents();
            return View(events);
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

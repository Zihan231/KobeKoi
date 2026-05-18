using KobeKoi.BLL.DTO;
using KobeKoi.BLL.Service.Events;
using KobeKoi.DAL.EF.Tables;
using KobeKoi.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KobeKoi.web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventRepo _eventRepo;
        private readonly VenueRepo _venueRepo;
        private readonly EventService _eventService;

        public EventController (EventRepo eventRepo, VenueRepo venueRepo, EventService eventService)
        {
            _eventRepo = eventRepo;
            _venueRepo = venueRepo;
            _eventService = eventService;
        }

        //Show Form
        public IActionResult CreateEvent()
        {
            var organizerId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            if (!organizerId.HasValue || role != 2)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }

            ViewBag.Venues = _venueRepo.GetAll().ToList();
            return View();
        }

        //Process Data
        [HttpPost]
        public IActionResult CreateEvent(CreateEventDTO dto)
        {
            ViewBag.Venues = _venueRepo.GetAll().ToList();

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var organizerId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            if (!organizerId.HasValue || role != 2)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }

            var venue = _venueRepo.GetAll()
                .FirstOrDefault(v => v.Id == dto.VenueId);

            if (venue == null)
            {
                ModelState.AddModelError("VenueId", "Invalid venue selected");
                return View(dto);
            }

            if (dto.MaxCapacity > venue.CapacityLimit)
            {
                ModelState.AddModelError("MaxCapacity",
                    $"Maximum allowed capacity is {venue.CapacityLimit}");

                return View(dto);
            }

            var result = _eventService.CreateEvent(dto, organizerId.Value);

            if (!result)
            {
                ModelState.AddModelError("", "Event creation failed");
                return View(dto);
            }

            return RedirectToAction("CreateEvent");
        }

        //my events
        public IActionResult MyEvents()
        {
            var organizerId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            if (!organizerId.HasValue || role != 2)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }

            var events = _eventService.GetEventsByOrganizerId(organizerId.Value);

            return View(events);
        }

        public IActionResult PayVenue(int id)
        {
            var result = _eventService.PayVenue(id);

            if (!result)
            {
                TempData["Error"] = "Payment failed";
                return RedirectToAction("MyEvents");
            }

            TempData["Success"] = "Payment successful";
            return RedirectToAction("MyEvents");
        }
    }
}

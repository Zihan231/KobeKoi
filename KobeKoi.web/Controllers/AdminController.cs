using KobeKoi.BLL.Service.Admin;
using KobeKoi.BLL.Service.Events;
using KobeKoi.BLL.Service.Users;
using KobeKoi.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KobeKoi.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        //Auth helper
        private bool IsAdmin()
        {
            var AdminID = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            if (AdminID == null || role != 3)
            {
                HttpContext.Session.Clear();
                return false;
            }
            return true;
        }

        //show status
        public IActionResult AdminStats()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "User");
            }
            var stats = _adminService.GetAdminStats();
            return View(stats);
        }

        //see all events
        public IActionResult AllEvents()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "User");
            }
            var events = _adminService.GetAllEvents();
            return View(events);
        }

        //update status of an event
        [HttpPost]
        public IActionResult UpdateStatus(int id, int status)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "User");
            }
            _adminService.UpdateEventStatus(id, status);
            return RedirectToAction("AllEvents");
        }

        //Delete an event
        [HttpPost]
        public IActionResult DeleteEvent(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "User");
            }
            _adminService.DeleteEvent(id);
            return RedirectToAction("AllEvents");
        }
    }
}

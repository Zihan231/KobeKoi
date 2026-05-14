using Microsoft.AspNetCore.Mvc;

namespace KobeKoi.web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //Login
        public IActionResult Login()
        {
            return View();
        }



        //SignUp
        public IActionResult SignUp()
        {
            return View();
        }
    }
}

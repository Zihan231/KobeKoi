using KobeKoi.BLL.DTO;
using KobeKoi.BLL.Service.Auth;
using KobeKoi.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KobeKoi.web.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthService _authService;
        private readonly UserRepo _userRepo;
        public UserController(AuthService authService, UserRepo userRepo)
        {
            _authService = authService;
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }


        //Login form show
        public IActionResult Login()
        {
            return View();
        }
        //Login data process
        [HttpPost]
        public IActionResult Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            string result = _authService.AuthenticateUser(loginDto);

            if (result == "Success")
            {
                var user = _userRepo.GetAll().FirstOrDefault(u => u.Email.ToLower() == loginDto.Email.ToLower());

                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetInt32("UserRole", user.Role);

                return RedirectToAction("Dashboard", "User");
            }

            ViewBag.Message = result;
            return View(loginDto);
        }

        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        //SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        //SignUp Data process
        [HttpPost]
        public IActionResult SignUp(CreateUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = _authService.CreateUser(dto);

            if (result)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "User creation failed";
            return View(dto);
        }

        //Login check Function
        private bool IsLoggedIn()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var email = HttpContext.Session.GetString("UserEmail");

            if (userId.HasValue && !string.IsNullOrWhiteSpace(email))
            {
                return true;
            }
            return false;
        }

        //Dashboard
        public IActionResult Dashboard()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}

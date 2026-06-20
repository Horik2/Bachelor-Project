using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplikacjaDeklaracji.Models;
using AplikacjaDeklaracji.Services;
using BCrypt;
using BCrypt.Net;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Identity;


namespace AplikacjaDeklaracji.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LogService _logService;

        public UserController(ApplicationDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult AddUser([FromForm] User model)
        {
            if (_context.User.Any(u => u.Username == model.Username))
            {
                LogService.AddLog(0, (TypZdarzenia)1, "Błąd: rejestracja nie powiodła się", "Nazwa użytkownika jest zajęta");

                TempData["Alert"] = "Nazwa użytkownika jest zajęta";
                return RedirectToAction("Register");
            }
            else
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash, salt);

                _context.User.Add(new User
                {
                    Username = model.Username,
                    PasswordHash = hashedPassword,
                    Salt = salt
                });

                _context.SaveChanges();


                string username = model.Username;
                var user = _context.User.FirstOrDefault(u => u.Username == username);
                if (user == null) 
                {
                    LogService.AddLog(0, (TypZdarzenia)1, "Błąd: rejestracja nie powiodła się", "Wystąpił niezidentyfikowany błąd");

                    TempData["Alert"] = "Wystąpił niezidentyfikowany błąd";
                    return RedirectToAction("Register");
                }
                int id = user.Id;

                LogService.AddLog(id, 0, "Pomyślnie utworzono nowego uzytkownika", "");

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromForm] User model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null) 
            {
                LogService.AddLog(0, (TypZdarzenia)1, "Błąd: logowanie nie powiodło się", "Podano nieistniejącego użytkownika");
                TempData["Alert"] = "Nie ma takiego użytkownika";
                return RedirectToAction("Login");
            }

            int id = user.Id;

            if (user != null && BCrypt.Net.BCrypt.Verify(model.PasswordHash, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                var principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);


                LogService.AddLog(id, 0, "Pomyślnie zalogowano", "");

                TempData["Alert"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LogService.AddLog(id, (TypZdarzenia)1, "Błąd: logowanie nie powiodło się", "Podano błędne hasło");

                TempData["Alert"] = "Błędne dane logowania";
                return RedirectToAction("Login");
            }
        }

        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        
    }
}
using Microsoft.AspNetCore.Mvc;
using AplikacjaDeklaracji.Models;
using AplikacjaDeklaracji.Services;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace AplikacjaDeklaracji.Controllers
{
    public class TabelaDeklaracjiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LogService _logService;

        public TabelaDeklaracjiController(ApplicationDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }


        public void UpdateIsActualForAllUsers()
        {
            var users = _context.User.Include(u => u.Deklaracje).ToList();

            foreach (var user in users)
            {
                var latestDeclaration = user.Deklaracje.OrderByDescending(d => d.Data).FirstOrDefault();

                foreach (var declaration in user.Deklaracje)
                {
                    declaration.IsActual = declaration == latestDeclaration;
                }
            }

            _context.SaveChanges();
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddForm(DaneOsobowe dane)
        {
            string username = HttpContext.User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            if (user == null) { return NotFound(); }
            int id = user.Id;

            var existingPerson = _context.daneOsobowes.FirstOrDefault(d => d.Pesel == dane.Pesel);
            if (existingPerson != null)
            {
                LogService.AddLog(id, (TypZdarzenia)2, "Ostrzerzenie: Dane osobowe już istnieją", "Wystąpiło powtórzenie numeru pesel");

            }

            dane.Lokale = dane.Lokale ?? new List<Lokale>();

            foreach (var lokal in dane.Lokale)
            {
                _context.Lokales.Add(lokal);
            }

            _context.daneOsobowes.Add(dane);


            var deklaracja = new Deklaracja
            {
                Data = DateTime.Now, 
                IsActual = true,     
                DaneOsobowe = dane,  
                User = user,         
                UserId = id
            };

            _context.Deklaracjas.Add(deklaracja);


            _context.SaveChanges();

            UpdateIsActualForAllUsers();

            LogService.AddLog(id, 0, "Pomyślnie dodano do bazy", "Utworzono deklarację");

            return RedirectToAction("Index", "Home");
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using AplikacjaDeklaracji.Models;
using AplikacjaDeklaracji.Services;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Xml.Linq;

namespace AplikacjaDeklaracji.Controllers
{
    public class HistoriaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly LogService _logService;

        public HistoriaController(ApplicationDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public IActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;

            var user = _context.User.Include(u => u.Deklaracje).FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user.Deklaracje);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string username = HttpContext.User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            if (user == null) { return NotFound(); }
            int iduser = user.Id;


            var declaration = _context.Deklaracjas
                .Include(d => d.DaneOsobowe)
                    .ThenInclude(d => d.Lokale)
                .FirstOrDefault(d => d.Id == id);

            if (declaration == null)
            {
                return NotFound();
            }

            
            if (declaration.UserId != iduser)
            {
                return Forbid();
            }

            return View(declaration);
        }

        [HttpPost]
        public IActionResult Edit(Deklaracja declaration)
        {
            string username = HttpContext.User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }
            int iduser = user.Id;

            var existingDeclaration = _context.Deklaracjas
                .Include(d => d.DaneOsobowe)
                .ThenInclude(d => d.Lokale)
                .FirstOrDefault(d => d.Id == declaration.Id);

            if (existingDeclaration == null)
            {
                return NotFound();
            }

            if (existingDeclaration.UserId != iduser)
            {
                return Forbid(); 
            }

            existingDeclaration.DaneOsobowe.Pesel = declaration.DaneOsobowe.Pesel;
            existingDeclaration.DaneOsobowe.Nazwisko = declaration.DaneOsobowe.Nazwisko;
            existingDeclaration.DaneOsobowe.Imie = declaration.DaneOsobowe.Imie;
            existingDeclaration.DaneOsobowe.DrugieImie = declaration.DaneOsobowe.DrugieImie;
            existingDeclaration.DaneOsobowe.ImieOjca = declaration.DaneOsobowe.ImieOjca;
            existingDeclaration.DaneOsobowe.DataUrodzenia = declaration.DaneOsobowe.DataUrodzenia;
            existingDeclaration.DaneOsobowe.PelnaNazwa = declaration.DaneOsobowe.PelnaNazwa;
            existingDeclaration.DaneOsobowe.IdentyfikatorRegon = declaration.DaneOsobowe.IdentyfikatorRegon;
            existingDeclaration.DaneOsobowe.IdentyfikatorNip = declaration.DaneOsobowe.IdentyfikatorNip;
            existingDeclaration.DaneOsobowe.NrTelefonu = declaration.DaneOsobowe.NrTelefonu;
            existingDeclaration.DaneOsobowe.AdresEMail = declaration.DaneOsobowe.AdresEMail;


            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditLokal(int id)
        {
            var lokal = _context.Lokales.FirstOrDefault(l => l.Id == id);
            if (lokal == null)
            {
                return NotFound();
            }

            return View(lokal);
        }

        [HttpPost]
        public IActionResult EditLokal(Lokale lokal)
        {
            var existingLokal = _context.Lokales.FirstOrDefault(l => l.Id == lokal.Id);
            if (existingLokal == null)
            {
                return NotFound();
            }

            existingLokal.Kraj = lokal.Kraj;
            existingLokal.Wojewodztwo = lokal.Wojewodztwo;
            existingLokal.Powiat = lokal.Powiat;
            existingLokal.Gmina = lokal.Gmina;
            existingLokal.Ulica = lokal.Ulica;
            existingLokal.NrDomu = lokal.NrDomu;
            existingLokal.NrLokalu = lokal.NrLokalu;
            existingLokal.Miejscowosc = lokal.Miejscowosc;
            existingLokal.KodPocztowy = lokal.KodPocztowy;
            existingLokal.StawkaZaOs = lokal.StawkaZaOs;
            existingLokal.LiczbaOsob = lokal.LiczbaOsob;
            existingLokal.KwotaZwolnienia = lokal.KwotaZwolnienia;
            existingLokal.WysokoscOplaty = lokal.WysokoscOplaty;
            existingLokal.WysokoscPoZwol = lokal.WysokoscPoZwol;
            existingLokal.KosztOstateczny = lokal.KosztOstateczny;
            existingLokal.KwartalnaOplata = lokal.KwartalnaOplata;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var declaration = _context.Deklaracjas.FirstOrDefault(d => d.Id == id);
            if (declaration == null)
            {
                return NotFound();
            }

            _context.Deklaracjas.Remove(declaration);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}

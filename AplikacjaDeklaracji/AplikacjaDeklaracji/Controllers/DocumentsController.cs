using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using AplikacjaDeklaracji.Models;
using ServiceReference1;
using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AplikacjaDeklaracji.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly pullClient _pullClient;
        
        public DocumentsController(pullClient pullClient)
        {
            _pullClient = pullClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _pullClient.oczekujaceDokumentyAsync(new ServiceReference1.ZapytaniePullOczekujaceTyp());

                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}

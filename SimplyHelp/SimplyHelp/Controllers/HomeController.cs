using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SimplyHelp.Controllers
{

    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {

            ViewData["MyKey"] = Configuration.GetSection("Api").GetSection("Key");
            var myKey = ViewData["MyKey"];

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Tweet()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult TermofUse()
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

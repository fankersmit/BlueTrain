using System.Diagnostics;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TerminalDashboard.Models;

namespace TerminalDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            // get all terminals from config
            var path = _environment.WebRootPath;
            var file = Path.Combine(path, "terminals.json");
            var jsonString = System.IO.File.ReadAllText(file);
            var terminals = JsonSerializer.Deserialize<TerminalAddress[]>(jsonString);

            ViewData["Title"] = "Terminals overview";
            ViewData["TerminalCount"] = terminals.Length;
            foreach (var ta in terminals)
            {
                ViewData[ta.Name] = ta.Address;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}

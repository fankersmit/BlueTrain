using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TerminalDashboard.Models;
using System.Text.Json;

namespace TerminalDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly TerminalAddressModel[] _terminals;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;

            // get all terminals from config
            var path = _environment.WebRootPath;
            var file = Path.Combine(path, "terminals.json");
            var jsonString = System.IO.File.ReadAllText(file);
            _terminals = JsonSerializer.Deserialize<TerminalAddressModel[]>(jsonString);
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Terminals overview";
            ViewData["TerminalCount"] = _terminals.Length;
            foreach (var ta in _terminals)
            {
                ViewData[ta.Name] = ta.Address;
            }
            return View();
        }

        // get terminal info for terminal with TerminalId in ViewData
        public async Task<ActionResult> TerminalInformation( string terminalID )
        {
            var currentTerminal = _terminals.First(c => c.Name == terminalID);
            var apiRequest = $"{currentTerminal.Address}api/v1/terminal/information";
            TerminalModel terminalModel = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(apiRequest);

               if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    terminalModel = JsonSerializer.Deserialize<TerminalModel>(response);

                }
            }

            return View(terminalModel);
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using consumer.Models;
using Steeltoe.Discovery.Client;
using System.Net.Http;

namespace consumer.Controllers
{


    public class HomeController : Controller
    {
        private DiscoveryHttpClientHandler _handler;

        public HomeController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
        }


        public IActionResult Index()
        {

            HttpClient c = new HttpClient(_handler, false);

            //call service using registered alias
            string s = c.GetStringAsync("http://dotnet-demo-service").Result;

            ViewData["CallResult"] = "Service result is: " + s;

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

        public IActionResult Privacy()
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

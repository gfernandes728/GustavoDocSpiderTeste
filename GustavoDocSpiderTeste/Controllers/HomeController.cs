using System.Diagnostics;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GustavoDocSpiderTeste.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
            => View();

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
    }
}

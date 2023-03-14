using AdBoardsWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdBoardsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddAdPage()
        {
            return View();
        }

        public IActionResult AdPage()
        {
            return View();
        }

        public IActionResult AdsPage()
        {
            return View();
        }

        public IActionResult AuthorizationPage()
        {
            return View();
        }

        public IActionResult EditingProfilePage()
        {
            return View();
        }

        public IActionResult FavoritesAdsPage()
        {
            return View();
        }

        public IActionResult MyAdsPage()
        {
            return View();
        }

        public IActionResult ProfilePage()
        {
            return View();
        }

        public IActionResult RecoveryPasswordPage()
        {
            return View();
        }

        public IActionResult RegistrationPage()
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
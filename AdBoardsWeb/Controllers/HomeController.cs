using AdBoardsWeb.Models;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

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
            Context.AdNow = new Ad();
            if (Context.UserNow == null)
            {
                return View("AuthorizationPage");
            }
            return View(Context.AdNow);
        }

        public IActionResult AdPage()
        {
            return View();
        }

        public async Task<IActionResult> AdsPage()
        {
            AdListViewModel adList = new AdListViewModel();

            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/Ads/GetAds");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                adList.AdList = JsonSerializer.Deserialize<List<Ad>>(responseContent);

                return View(adList);
            }
            else
            {
                return View();
            }
        }

        public IActionResult AuthorizationPage()
        {
            return View();
        }

        public IActionResult EditingProfilePage()
        {
            if (Context.UserNow == null)
            {
                return View("AuthorizationPage");
            }
            return View();
        }

        public IActionResult FavoritesAdsPage()
        {
            if (Context.UserNow == null)
            {
                return View("AuthorizationPage");
            }
            return View();
        }

        public async Task<IActionResult> MyAdsPage()
        {
            if (Context.UserNow == null)
            {
                return View("AuthorizationPage");
            }
            else
            {
                AdListViewModel adList = new AdListViewModel();

                var httpClient = new HttpClient();
                using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    adList.AdList = JsonSerializer.Deserialize<List<Ad>>(responseContent);

                    return View(adList);
                }
                else
                {
                    return View();
                }
            }
        }

        public IActionResult ProfilePage()
        {
            if (Context.UserNow == null)
            {
                return View("AuthorizationPage");
            }
            else
            {
                Person user = Context.UserNow!;
                return View("~/Views/Home/ProfilePage.cshtml", user);
            }
        }

        public IActionResult RecoveryPasswordPage()
        {
            return View();
        }

        public IActionResult RegistrationPage()
        {
            Person p = new Person();
            return View(p);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
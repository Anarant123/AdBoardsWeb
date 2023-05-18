using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdBoardsWeb.Controllers
{
    public class AdPageController : Controller
    {
        private readonly ILogger<AdPageController> _logger;

        public AdPageController(ILogger<AdPageController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> AdToFavorite(int Id)
        {
            if (Context.UserNow == null)
            {
                ViewBag.Result = 3;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/Favorites/Addition?AdId={Id}&PersonId={Context.UserNow.Id}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Result = 1;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
            else
            {
                ViewBag.Result = 2;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
        }

        public async Task<IActionResult> ToComplain(int Id)
        {
            if (Context.UserNow == null)
            {
                ViewBag.Result = 3;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/Complaint/Addition?AdId={Id}&PersonId={Context.UserNow.Id}");
            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Result = 4;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
            else
            {
                ViewBag.Result = 5;
                return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
            }
        }
    }
}

using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers
{
    public class AdsPageController : Controller
    {
        private readonly ILogger<AdsPageController> _logger;

        public AdsPageController(ILogger<AdsPageController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult>OpenAd(string Id)
        {
            Context.AdNow = Context.AdList.Ads.First(x => x.Id == Convert.ToInt32(Id));

            if (Context.UserNow != null)
            {
                var httpClient = new HttpClient();
                using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Favorites/IsFavorite?AdId={Context.AdNow.Id}&PersonId={Context.UserNow.Id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    ViewBag.IsFavorites = 1;
                else
                    ViewBag.IsFavorites = 2;
            }
            else
            {
                ViewBag.IsFavorites = 2;
            }


            return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
        }
    }
}

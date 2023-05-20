using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        public async Task<IActionResult> OpenMyAd(string Id)
        {
            Context.AdNow = Context.AdList.Ads.First(x => x.Id == Convert.ToInt32(Id));
            Context.AdNow.Person = Context.UserNow;

            return View("~/Views/Home/MyAdPage.cshtml", Context.AdNow);
        }

        public async Task<IActionResult> ApplyFilters(string v, string From, string To, string City, string Category, string buyOrSell)
        {
            bool result;
            string responseContent;

            if (v == "ads")
            {
                var httpClient = new HttpClient();
                using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5228/Ads/GetAds");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                responseContent = await response.Content.ReadAsStringAsync();
                result = response.IsSuccessStatusCode;
            }
            else if (v == "myads")
            {
                var httpClient = new HttpClient();
                using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                responseContent = await response.Content.ReadAsStringAsync();
                result = response.IsSuccessStatusCode;
            }
            else
            {
                var httpClient = new HttpClient();
                using HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetFavoritesAds?id={Context.UserNow.Id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                responseContent = await response.Content.ReadAsStringAsync();
                result = response.IsSuccessStatusCode;
            }

            if (result)
            {
                Context.AdList = new AdListViewModel();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Используйте это, если нужно преобразование в camelCase
                    IgnoreNullValues = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);

                if(!string.IsNullOrEmpty(From))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price >= Convert.ToInt32(From)).ToList();
                if(!string.IsNullOrEmpty(To))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.Price <= Convert.ToInt32(To)).ToList();
                if(!string.IsNullOrEmpty(City))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.City == City).ToList();
                if(!string.IsNullOrEmpty(Category) && Convert.ToInt32(Category) != 0)
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.CotegorysId == Convert.ToInt32(Category)).ToList();
                if(!string.IsNullOrEmpty(buyOrSell))
                    Context.AdList.Ads = Context.AdList.Ads.Where(x => x.TypeOfAdId == Convert.ToInt32(buyOrSell)).ToList();


                ViewBag.IsSuccess = true;
                if (v == "ads")
                    return View("~/Views/Home/AdsPage.cshtml", Context.AdList);
                else if (v == "myads")
                    return View("~/Views/Home/MyAdsPage.cshtml", Context.AdList);
                else
                    return View("~/Views/Home/FavoritesAdsPage.cshtml", Context.AdList);
            }
            else
            {
                return View();
            }
        }
    }
}

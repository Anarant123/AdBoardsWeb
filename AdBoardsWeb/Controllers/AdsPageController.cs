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
        public IActionResult OpenAd(string Id)
        {
            Context.AdNow = Context.AdList.Ads.First(x => x.Id == Convert.ToInt32(Id));

            return View("~/Views/Home/AdPage.cshtml", Context.AdNow);
        }
    }
}

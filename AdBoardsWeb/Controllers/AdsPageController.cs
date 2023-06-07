using AdBoards.ApiClient;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class AdsPageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<AdsPageController> _logger;

    public AdsPageController(ILogger<AdsPageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    [AllowAnonymous]
    public async Task<IActionResult> OpenAd(int id)
    {
        var ad = await _api.GetAd(id);
        if (ad is null) return NotFound();

        return View("~/Views/Home/AdPage.cshtml", ad);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ApplyFilters(AdsView model)
    {
        var filter = model.Filter;
        var rbBuy = filter.AdTypeId == 1;
        var rbSell = filter.AdTypeId == 2;

        var ads = await _api.UseFulter(filter.Type, filter.PriceFrom, filter.PriceUpTo, filter.City,
            filter.CategoryId, rbBuy, rbSell);
        var adsView = new AdsView { Ads = ads };

        return filter.Type switch
        {
            1 => View("~/Views/Home/AdsPage.cshtml", adsView),
            2 => View("~/Views/Home/MyAdsPage.cshtml", adsView),
            _ => View("~/Views/Home/FavoritesAdsPage.cshtml", adsView)
        };
    }
}
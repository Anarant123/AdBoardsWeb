using System.Diagnostics;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public IActionResult AddAdPage()
    {
        var ad = new AddAdModel();
        return View(ad);
    }

    [AllowAnonymous]
    public IActionResult AdPage()
    {
        return View();
    }

    [AllowAnonymous]
    public async Task<IActionResult> AdsPage()
    {
        var ads = await _api.GetAds();
        return View(new AdsView { Ads = ads });
    }

    [AllowAnonymous]
    [Route("Authorization")]
    public IActionResult AuthorizationPage()
    {
        return View();
    }

    public IActionResult EditProfilePage()
    {
        return View();
    }

    public async Task<IActionResult> FavoritesAdsPage()
    {
        var ads = await _api.GetFavoritesAds();
        var adsView = new AdsView { Ads = ads };
        return View(adsView);
    }

    public async Task<IActionResult> MyAdsPage()
    {
        var ads = await _api.GetMyAds();
        var adsView = new AdsView { Ads = ads };
        ViewBag.IsSuccess = false;
        return View(adsView);
    }

    public async Task<IActionResult> ProfilePage()
    {
        var person = await _api.GetMe();
        return View("~/Views/Home/ProfilePage.cshtml", person);
    }

    [AllowAnonymous]
    public IActionResult RecoveryPasswordPage()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult RegistrationPage()
    {
        var p = new PersonReg();
        return View(p);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
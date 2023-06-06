using System.Diagnostics;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Responses;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

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
        /*Context.AdNow = new Ad();
        if (Context.UserNow == null) return View("AuthorizationPage");
        return View(Context.AdNow);*/
        throw new Exception();
    }

    public IActionResult AdPage()
    {
        return View();
    }

    public async Task<IActionResult> AdsPage()
    {
        /*Context.AdList = new AdListViewModel();

        var ads = await _api.GetAds();

        Context.AdList.Ads = ads;

        return View(Context.AdList);*/
        throw new Exception();
    }

    public IActionResult AuthorizationPage()
    {
        return View();
    }

    public IActionResult EditingProfilePage()
    {
        /*if (Context.UserNow == null) return View("AuthorizationPage");*/
        return View();
    }

    public async Task<IActionResult> FavoritesAdsPage()
    {
        /*if (Context.UserNow == null) return View("AuthorizationPage");

        var httpClient = new HttpClient();
        using var response =
            await httpClient.GetAsync($"http://localhost:5228/Ads/GetFavoritesAds?id={Context.UserNow.Id}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Context.AdList = new AdListViewModel();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent, options);

            ViewBag.IsSuccess = true;
            return View(Context.AdList);
        }

        ViewBag.IsSuccess = false;*/
        return View();
    }

    public async Task<IActionResult> MyAdsPage()
    {
        /*if (Context.UserNow == null) return View("AuthorizationPage");

        var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Context.AdList = new AdListViewModel();

            Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent);

            ViewBag.IsSuccess = true;
            return View(Context.AdList);
        }

        ViewBag.IsSuccess = false;*/
        return View();
    }

    public async Task<IActionResult> ProfilePage()
    {
        var person = await _api.GetMe();
        return View("~/Views/Home/ProfilePage.cshtml", person);
    }

    public IActionResult RecoveryPasswordPage()
    {
        return View();
    }

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
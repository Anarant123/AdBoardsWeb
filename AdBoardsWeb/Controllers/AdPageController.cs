using AdBoards.ApiClient;
using AdBoards.ApiClient.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class AdPageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<AdPageController> _logger;

    public AdPageController(ILogger<AdPageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    private void SetUpViewBag(bool? addedToFavorites = null, bool? deletedFromFavorites = null, bool? reported = null)
    {
        ViewBag.AddedToFavorites = addedToFavorites!;
        ViewBag.DeletedFromFavorites = deletedFromFavorites!;
        ViewBag.Reported = reported!;
    }

    public async Task<IActionResult> AdToFavorite(int id)
    {
        var added = await _api.AddToFavorites(id);
        SetUpViewBag(added);
        return RedirectToAction("OpenAd", "AdsPage", new { id });
    }

    public async Task<IActionResult> DeleteFromFavorite(int id)
    {
        var deleted = await _api.DeleteFromFavorites(id);
        SetUpViewBag(deletedFromFavorites: deleted);
        return RedirectToAction("OpenAd", "AdsPage", new { id });
    }

    public async Task<IActionResult> ToComplain(int id)
    {
        var reported = await _api.AddToComplaints(id);
        SetUpViewBag(reported: reported);
        return RedirectToAction("OpenAd", "AdsPage", new { id });
    }
}
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class AddAdPageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<AddAdPageController> _logger;

    public AddAdPageController(ILogger<AddAdPageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> AddAd(AddAdModel model)
    {
        var ad = await _api.AddAd(model);
        if (ad is null) return View("~/Views/Home/AddAdPage.cshtml", model);

        model.Id = ad.Id;

        await _api.UpdateAdPhoto(model);

        return RedirectToAction("Index", "MyAdPage", new { ad.Id });
    }

    public async Task<IActionResult> SetPhoto(AddAdModel model)
    {
        var res = await _api.UpdateAdPhoto(model);

        return View("~/Views/Home/AddAdPage.cshtml", model);
    }
}
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class MyAdPageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<MyAdPageController> _logger;

    public MyAdPageController(ILogger<MyAdPageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> Index(int id)
    {
        var ad = await _api.GetAd(id);
        if (ad is null) return NotFound();

        var addModel = new AddAdModel
        {
            Id = ad.Id,
            Name = ad.Name,
            Description = ad.Description,
            Price = ad.Price,
            CategoryId = ad.Category.Id,
            City = ad.City,
            AdTypeId = ad.AdType.Id,
            PhotoName = ad.PhotoName
        };

        return View("~/Views/Home/MyAdPage.cshtml", addModel);
    }

    public async Task<IActionResult> UpdatePhoto(AddAdModel model)
    {
        var ad = await _api.UpdateAdPhoto(model);
        if (ad is null) return View("~/Views/Home/MyAdPage.cshtml", model);

        return RedirectToAction("Index", "MyAdPage", new { ad.Id });
    }

    public async Task<IActionResult> SaveChanges(AddAdModel model)
    {
        var ad = await _api.AdUpdate(model);
        if (ad is null) return View("~/Views/Home/MyAdPage.cshtml", model);

        return RedirectToAction("Index", "MyAdPage", new { ad.Id });
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAd(id);
        return RedirectToAction("MyAdsPage", "Home");
    }
}
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class AdsController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<AdsController> _logger;

    public AdsController(ILogger<AdsController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var ads = await _api.GetAds();
        return View(new AdsViewModel { Ads = ads });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Ad(int id)
    {
        var ad = await _api.GetAd(id);
        if (ad is null) return NotFound();

        return View(ad);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ApplyFilters(AdsViewModel model)
    {
        var filter = model.Filter;
        var rbBuy = filter.AdTypeId == 1;
        var rbSell = filter.AdTypeId == 2;

        var ads = await _api.UseFulter(filter.Type, filter.PriceFrom, filter.PriceUpTo, filter.City,
            filter.CategoryId, rbBuy, rbSell);
        var adsView = new AdsViewModel { Ads = ads };

        return filter.Type switch
        {
            1 => View("Index", adsView),
            2 => View("My", adsView),
            _ => View("Favorites", adsView)
        };
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAdViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var model = new AddAdModel
        {
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price!.Value,
            City = vm.City,
            CategoryId = vm.CategoryId,
            AdTypeId = vm.AdTypeId,
            Photo = vm.Photo
        };

        var result = await _api.AddAd(model);
        if (result is null) return View(vm);

        model.Id = result.Id;

        await _api.UpdateAdPhoto(model);

        return RedirectToAction("MyAd", "Ads", new { result.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        var ads = await _api.GetFavoritesAds();
        var adsView = new AdsViewModel { Ads = ads };
        return View(adsView);
    }

    [HttpGet]
    public async Task<IActionResult> My()
    {
        var ads = await _api.GetMyAds();
        var adsView = new AdsViewModel { Ads = ads };
        ViewBag.IsSuccess = false;
        return View(adsView);
    }

    public async Task<IActionResult> AdToFavorites(int id)
    {
        var _ = await _api.AddToFavorites(id);
        return RedirectToAction("Ad", new { id });
    }

    public async Task<IActionResult> DeleteFromFavorites(int id)
    {
        var _ = await _api.DeleteFromFavorites(id);
        return RedirectToAction("Ad", new { id });
    }

    public async Task<IActionResult> Complain(int id)
    {
        var _ = await _api.AddToComplaints(id);
        return RedirectToAction("Ad", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> MyAd(int id)
    {
        var ad = await _api.GetAd(id);
        if (ad is null) return NotFound();

        var addModel = new CreateAdViewModel
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

        return View("MyAd", addModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> MyAd(CreateAdViewModel vm)
    {
        var model = new AddAdModel
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price!.Value,
            City = vm.City,
            AdTypeId = vm.AdTypeId,
            CategoryId = vm.CategoryId,
            Photo = vm.Photo
        };

        var ad = await _api.AdUpdate(model);
        if (ad is null) return View("MyAd", vm);
        
        return RedirectToAction("MyAd", new { ad.Id });
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdatePhoto(CreateAdViewModel vm)
    {
        var model = new AddAdModel
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price!.Value,
            City = vm.City,
            AdTypeId = vm.AdTypeId,
            CategoryId = vm.CategoryId,
            PhotoName = vm.PhotoName,
            Photo = vm.Photo
        };

        var ad = await _api.UpdateAdPhoto(model);
        if (ad is null) return View("MyAd", vm);

        return RedirectToAction("MyAd", new { ad.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAd(id);
        return RedirectToAction("My");
    }
}
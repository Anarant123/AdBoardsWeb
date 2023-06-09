using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var person = await _api.GetMe();
        if (person is null) return NotFound();

        var vm = new ProfileViewModel
        {
            Login = person.Login,
            Name = person.Name,
            Email = person.Email,
            Birthday = person.Birthday,
            Phone = person.Phone,
            City = person.City,
            PhotoName = person.PhotoName
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var person = await _api.GetMe();
        if (person is null) return NotFound();

        var vm = new EditProfileViewModel
        {
            Name = person.Name,
            Email = person.Email,
            Birthday = person.Birthday,
            Phone = person.Phone,
            City = person.City,
            PhotoName = person.PhotoName
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProfileViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var model = new EditPersonModel
        {
            Name = vm.Name,
            Email = vm.Email,
            Birthday = vm.Birthday!.Value,
            Phone = vm.Phone,
            City = vm.City
        };

        var result = await _api.PersonUpdate(model);

        if (result.IsOk) return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Неудачная попытка обновления данных профиля");
        foreach (var error in result.Error) ModelState.AddModelError(string.Empty, error.Message);

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Photo(EditProfileViewModel vm)
    {
        var model = new EditPersonModel
        {
            Name = vm.Name,
            Email = vm.Email,
            Birthday = vm.Birthday!.Value,
            Phone = vm.Phone,
            City = vm.City,
            Photo = vm.Photo
        };
        
        var result = await _api.UpdatePersonPhoto(model);
        if (result is null) return View("Edit", vm);

        return RedirectToAction("Index");
    }
}
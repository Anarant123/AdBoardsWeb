using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class EditingProfilePageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<EditingProfilePageController> _logger;

    public EditingProfilePageController(ILogger<EditingProfilePageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> SetPhoto(EditPersonModel model)
    {
        var person = await _api.UpdatePersonPhoto(model);
        if (person is null) return View("~/Views/Home/EditProfilePage.cshtml", model);

        model = EditPersonModel.MapFromPerson(person);

        return View("~/Views/Home/EditProfilePage.cshtml", model);
    }

    public async Task<IActionResult> SaveProfileChanges(EditPersonModel model)
    {
        var person = await _api.PersonUpdate(model);
        if (person is null) return View("~/Views/Home/EditProfilePage.cshtml", model);

        model = EditPersonModel.MapFromPerson(person);

        return View("~/Views/Home/EditProfilePage.cshtml", model);
    }
}
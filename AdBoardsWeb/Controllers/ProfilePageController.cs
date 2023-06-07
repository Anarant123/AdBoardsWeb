using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class ProfilePageController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<ProfilePageController> _logger;

    public ProfilePageController(ILogger<ProfilePageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> EditProfilePage()
    {
        var me = await _api.GetMe();
        if (me is null) return Challenge();

        var model = EditPersonModel.MapFromPerson(me);

        return View("~/Views/Home/EditProfilePage.cshtml", model);
    }

    public async Task<IActionResult> Exit()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("AuthorizationPage", "Home");
    }
}
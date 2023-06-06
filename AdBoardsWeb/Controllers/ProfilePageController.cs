using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

public class ProfilePageController : Controller
{
    private readonly ILogger<ProfilePageController> _logger;

    public ProfilePageController(ILogger<ProfilePageController> logger)
    {
        _logger = logger;
    }

    public IActionResult EditingProfilePage()
    {
        /*var user = Context.UserNow!;

        return View("~/Views/Home/EditingProfilePage.cshtml", user);*/
        throw new Exception();
    }

    public IActionResult Exit()
    {
        /*Context.UserNow = null;*/

        return View("~/Views/Home/AuthorizationPage.cshtml");
    }
}
using System.Security.Claims;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Extensions;
using AdBoardsWeb.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

public class AuthorizationPageController : Controller
{
    private readonly ILogger<AuthorizationPageController> _logger;
    private readonly AdBoardsApiClient _api;

    public AuthorizationPageController(ILogger<AuthorizationPageController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> Authorization(string login, string password)
    {
        var model = await _api.Authorize(login, password);
        if (model is null) return View("~/Views/Home/AuthorizationPage.cshtml");

        var claims = new List<Claim>
        {
            new("jwt", model.Token)
        };
        var identity = new ClaimsIdentity(claims, AuthSchemes.Cookie);
        await HttpContext.SignInAsync(AuthSchemes.Cookie, new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });

        return RedirectToAction("ProfilePage", "Home");
    }

    public async Task<IActionResult> Recovery(string Login)
    {
        var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"http://localhost:5228/People/RecoveryPassword?Login={Login}");
        var response = await httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return View("~/Views/Home/AuthorizationPage.cshtml");
        return View("~/Views/Home/AuthorizationPage.cshtml");
    }
}
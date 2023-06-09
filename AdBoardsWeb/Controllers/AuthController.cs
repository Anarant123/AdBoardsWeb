using System.Security.Claims;
using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Extensions;
using AdBoards.Domain.Auth;
using AdBoardsWeb.Auth;
using AdBoardsWeb.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

[Authorize]
public class AuthController : Controller
{
    private readonly AdBoardsApiClient _api;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, AdBoardsApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("Authorization")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Authorization")]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid) return View(vm);

        var dto = await _api.Authorize(vm.Login, vm.Password);
        if (dto is null || dto.Kind == LoginResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Неудачная попытка входа. Введены неверные логин или пароль");
            return View(vm);
        }

        var claims = new List<Claim> { new("jwt", dto.Token) };
        var identity = new ClaimsIdentity(claims, AuthSchemes.Cookie);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(AuthSchemes.Cookie, new ClaimsPrincipal(identity), authProperties);

        returnUrl ??= "/";

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var person = new PersonReg
        {
            Login = vm.Login,
            Email = vm.Email,
            Birthday = vm.Birthday!.Value,
            Password = vm.Password,
            ConfirmPassword = vm.ConfirmPassword,
            Phone = vm.Phone
        };

        var result = (await _api.Registr(person)).ToList();

        if (result.Count == 0) return RedirectToAction("Login");

        ModelState.AddModelError(string.Empty, "Неудачная попытка регистрации");
        foreach (var error in result) ModelState.AddModelError(string.Empty, error.Message);

        return View(vm);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult RecoveryPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult RecoveryPassword(RecoveryPasswordViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Exit()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Ads");
    }
}